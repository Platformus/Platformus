// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Globalization;
using System.Text;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Barebone;
using Platformus.Configurations;
using Platformus.Forms.Data.Entities;
using Platformus.Forms.FormHandlers;

namespace Platformus.Forms.Frontend.FormHandlers
{
  public class EmailFormHandler : FormHandlerBase
  {
    public override IEnumerable<FormHandlerParameterGroup> ParameterGroups =>
      new FormHandlerParameterGroup[]
      {
        new FormHandlerParameterGroup(
          "General",
          new FormHandlerParameter("RecipientEmails", "Recipient emails (separated by commas)", "textBox", null, true),
          new FormHandlerParameter("RedirectUrl", "Redirect URL", "textBox")
        )
      };

    public override string Description => "Sends the form data to the specific email address.";

    protected override IActionResult GetActionResult(IRequestHandler requestHandler, Form form, IDictionary<Field, string> valuesByFields, IDictionary<string, byte[]> attachmentsByFilenames)
    {
      StringBuilder body = new StringBuilder();

      foreach (KeyValuePair<Field, string> valueByField in valuesByFields)
        body.AppendFormat("<p>{0}: {1}</p>", requestHandler.GetLocalizationValue(valueByField.Key.NameId), valueByField.Value);

      IEmailSender emailSender = requestHandler.HttpContext.RequestServices.GetService<IEmailSender>();

      if (emailSender != null)
      {
        string recipientEmails = this.GetStringParameterValue("RecipientEmails");

        if (!string.IsNullOrEmpty(recipientEmails))
          foreach (string recipientEmail in recipientEmails.Split(','))
            emailSender.SendEmail(
              recipientEmail,
              string.Format("{0} form data", requestHandler.GetLocalizationValue(form.NameId)),
              body.ToString(),
              attachmentsByFilenames
            );
      }

      string redirectUrl = this.GetStringParameterValue("RedirectUrl");

      if (this.GetConfigurationRoot(requestHandler)["Globalization:SpecifyCultureInUrl"] == "yes")
        redirectUrl = "/" + CultureInfo.CurrentCulture.TwoLetterISOLanguageName + redirectUrl;

      return (requestHandler as Controller).Redirect(redirectUrl);
    }

    private IConfigurationRoot GetConfigurationRoot(IRequestHandler requestHandler)
    {
      IConfigurationBuilder configurationBuilder = new ConfigurationBuilder().AddStorage(
        requestHandler.HttpContext.RequestServices.GetService<IStorage>()
      );

      return configurationBuilder.Build();
    }
  }
}