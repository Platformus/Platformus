// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using Platformus.Core.Services.Abstractions;

namespace Platformus.Core.Services.Defaults;

public class DefaultEmailSender : IEmailSender
{
  private IConfigurationManager configurationManager;

  public DefaultEmailSender(IConfigurationManager configurationManager)
  {
    this.configurationManager = configurationManager;
  }

  public async Task<SendEmailResult> SendEmailAsync(string to, string subject, string body, IDictionary<string, byte[]> attachmentsByFilenames)
  {
    string smtpServer = this.configurationManager["Email", "SmtpServer"];
    string smtpPort = this.configurationManager["Email", "SmtpPort"];
    string smtpUseSsl = this.configurationManager["Email", "SmtpUseSsl"];
    string smtpLogin = this.configurationManager["Email", "SmtpLogin"];
    string smtpPassword = this.configurationManager["Email", "SmtpPassword"];
    string smtpSenderEmail = this.configurationManager["Email", "SmtpSenderEmail"];
    string smtpSenderName = this.configurationManager["Email", "SmtpSenderName"];

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