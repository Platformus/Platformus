// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Core.Extensions;
using Platformus.Core.Parameters;
using Platformus.Core.Services.Abstractions;
using Platformus.Website.Data.Entities;
using Platformus.Website.FormHandlers;

namespace Platformus.Website.Frontend.FormHandlers
{
  public class EmailFormHandler : IFormHandler
  {
    public IEnumerable<ParameterGroup> ParameterGroups =>
      new ParameterGroup[]
      {
        new ParameterGroup(
          "General",
          new Parameter("RecipientEmails", "Recipient emails (separated by commas)", Core.JavaScriptEditorClassNames.TextBox, null, true),
          new Parameter("RedirectUrl", "Redirect URL", Core.JavaScriptEditorClassNames.TextBox)
        )
      };

    public string Description => "Sends the form data to the specific email address.";

    public async Task<IActionResult> HandleAsync(HttpContext httpContext, string origin, Form form, IDictionary<Field, string> valuesByFields, IDictionary<string, byte[]> attachmentsByFilenames)
    {
      StringBuilder body = new StringBuilder();

      foreach (KeyValuePair<Field, string> valueByField in valuesByFields)
        body.AppendFormat("<p>{0}: {1}</p>", valueByField.Key.Name.GetLocalizationValue(), valueByField.Value);

      ParametersParser parametersParser = new ParametersParser(form.Parameters);
      IEmailSender emailSender = httpContext.RequestServices.GetService<IEmailSender>();

      if (emailSender != null)
      {
        string recipientEmails = parametersParser.GetStringParameterValue("RecipientEmails");

        if (!string.IsNullOrEmpty(recipientEmails))
          foreach (string recipientEmail in recipientEmails.Split(','))
            await emailSender.SendEmailAsync(
              recipientEmail,
              string.Format("{0} form data", form.Name.GetLocalizationValue()),
              body.ToString(),
              attachmentsByFilenames
            );
      }

      string redirectUrl = parametersParser.GetStringParameterValue("RedirectUrl");

      if (httpContext.GetConfigurationManager()["Globalization", "SpecifyCultureInUrl"] == "yes")
        return new RedirectResult($"/{CultureInfo.CurrentCulture.TwoLetterISOLanguageName}{redirectUrl}");

      return new RedirectResult(redirectUrl);
    }
  }
}