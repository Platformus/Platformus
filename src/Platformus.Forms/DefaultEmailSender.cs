// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Platformus.Barebone;
using Platformus.Configurations;

namespace Platformus.Forms
{
  public class DefaultEmailSender : IEmailSender
  {
    IConfigurationRoot configurationRoot;

    public DefaultEmailSender(IStorage storage)
    {
      this.configurationRoot = new ConfigurationBuilder().AddStorage(storage).Build();
    }

    public SendEmailResult SendEmail(string to, string subject, string body, IDictionary<string, byte[]> attachmentsByFilenames)
    {
      string smtpServer = this.configurationRoot["Email:SmtpServer"];
      string smtpPort = this.configurationRoot["Email:SmtpPort"];
      string smtpLogin = this.configurationRoot["Email:SmtpLogin"];
      string smtpPassword = this.configurationRoot["Email:SmtpPassword"];
      string smtpSenderEmail = this.configurationRoot["Email:SmtpSenderEmail"];
      string smtpSenderName = this.configurationRoot["Email:SmtpSenderName"];

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
          smtpClient.Connect(smtpServer, int.Parse(smtpPort), false);
          smtpClient.AuthenticationMechanisms.Remove("XOAUTH2");
          smtpClient.Authenticate(smtpLogin, smtpPassword);
          smtpClient.Send(message);
          smtpClient.Disconnect(true);
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