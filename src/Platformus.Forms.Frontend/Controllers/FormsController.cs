// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Text;
using ExtCore.Data.Abstractions;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Platformus.Configuration;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Models;
using Platformus.Globalization.Data.Abstractions;

namespace Platformus.Forms.Frontend.Controllers
{
  [AllowAnonymous]
  public class FormsController : Platformus.Barebone.Frontend.Controllers.ControllerBase
  {
    private IConfigurationRoot configurationRoot;

    public FormsController(IStorage storage)
      : base(storage)
    {
      IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddStorage(storage);

      this.configurationRoot = configurationBuilder.Build();
    }

    [HttpPost]
    public IActionResult Send()
    {
      StringBuilder body = new StringBuilder();
      Form form = this.Storage.GetRepository<IFormRepository>().WithKey(int.Parse(this.Request.Form["formId"]));

      foreach (Field field in this.Storage.GetRepository<IFieldRepository>().FilteredByFormId(form.Id))
      {
        string value = this.Request.Form[string.Format("field{0}", field.Id)];

        // TODO: change the way the localized value is retrieved
        body.AppendFormat(
          "<p>{0}: {1}</p>",
          this.Storage.GetRepository<ILocalizationRepository>().FilteredByDictionaryId(field.NameId).First().Value,
          value
        );
      }

      this.SendEmail(form, body.ToString());

      // TODO: add RedirectUrl property to the Form class
      string referer = this.Request.Headers["Referer"];

      if (string.IsNullOrEmpty(referer))
        referer = "/";

      return this.Redirect(referer);
    }

    // TODO: we shouldn't use MailKit directly, need to replace with the service instead
    private void SendEmail(Form form, string body)
    {
      string smtpServer = this.configurationRoot["Email:SmtpServer"];
      string smtpPort = this.configurationRoot["Email:SmtpPort"];
      string smtpLogin = this.configurationRoot["Email:SmtpLogin"];
      string smtpPassword = this.configurationRoot["Email:SmtpPassword"];
      string smtpSenderEmail = this.configurationRoot["Email:SmtpSenderEmail"];
      string smtpSenderName = this.configurationRoot["Email:SmtpSenderName"];

      if (string.IsNullOrEmpty(smtpServer) || string.IsNullOrEmpty(smtpPort) || string.IsNullOrEmpty(smtpLogin) || string.IsNullOrEmpty(smtpPassword))
        return;

      MimeMessage message = new MimeMessage();

      message.From.Add(new MailboxAddress(smtpSenderName, smtpSenderEmail));
      message.To.Add(new MailboxAddress(form.Email, form.Email));

      // TODO: change the way the localized name is retrieved
      message.Subject = string.Format("{0} form data", this.Storage.GetRepository<ILocalizationRepository>().FilteredByDictionaryId(form.NameId).First().Value);

      BodyBuilder bodyBuilder = new BodyBuilder();

      bodyBuilder.HtmlBody = body;
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

      catch { }
    }
  }
}