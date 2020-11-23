// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Platformus.Core.Services.Abstractions;

namespace Platformus.Core.Services.Defaults
{
  public class DefaultEmailSender : IEmailSender
  {
    private IConfiguration configuration;

    public DefaultEmailSender(IStorage storage, IConfiguration configuration)
    {
      this.configuration = configuration;
    }

    public async Task<SendEmailResult> SendEmailAsync(string to, string subject, string body, IDictionary<string, byte[]> attachmentsByFilenames)
    {
      string smtpServer = this.configuration["Email:SmtpServer"];
      string smtpPort = this.configuration["Email:SmtpPort"];
      string smtpUseSsl = this.configuration["Email:SmtpUseSsl"];
      string smtpLogin = this.configuration["Email:SmtpLogin"];
      string smtpPassword = this.configuration["Email:SmtpPassword"];
      string smtpSenderEmail = this.configuration["Email:SmtpSenderEmail"];
      string smtpSenderName = this.configuration["Email:SmtpSenderName"];

      if (string.IsNullOrEmpty(smtpServer) || string.IsNullOrEmpty(smtpPort) || string.IsNullOrEmpty(smtpLogin) || string.IsNullOrEmpty(smtpPassword))
        return new SendEmailResult(false, "Email parameters are not set.");

      MimeMessage message = new MimeMessage();

      message.From.Add(new MailboxAddress(smtpSenderName, smtpSenderEmail));
      message.To.Add(new MailboxAddress(to, to));
      message.Subject = subject;

      BodyBuilder bodyBuilder = new BodyBuilder();

      bodyBuilder.HtmlBody = body;

      if (attachmentsByFilenames != null)
        foreach (KeyValuePair<string, byte[]> attachmentByFilename in attachmentsByFilenames)
          bodyBuilder.Attachments.Add(attachmentByFilename.Key, attachmentByFilename.Value);

      message.Body = bodyBuilder.ToMessageBody();

      try
      {
        using (SmtpClient smtpClient = new SmtpClient())
        {
          await smtpClient.ConnectAsync(smtpServer, int.Parse(smtpPort), string.Equals(smtpUseSsl, "yes", StringComparison.OrdinalIgnoreCase));
          smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
          await smtpClient.AuthenticateAsync(smtpLogin, smtpPassword);
          await smtpClient.SendAsync(message);
          await smtpClient.DisconnectAsync(true);
        }
      }

      catch (Exception e)
      {
        return new SendEmailResult(false, e.Message);
      }

      return new SendEmailResult(true);
    }
  }
}