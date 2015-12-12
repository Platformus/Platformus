// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Text;
using ExtCore.Data.Abstractions;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Platformus.Barebone.Frontend.Controllers;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Models;
using Platformus.Globalization.Data.Abstractions;

namespace Platformus.Forms.Frontend.Controllers
{
  [AllowAnonymous]
  public class FormsController : ControllerBase
  {
    public FormsController(IStorage storage)
      : base(storage)
    {
    }

    [HttpPost]
    public IActionResult Send()
    {
      StringBuilder body = new StringBuilder();
      Form form = this.Storage.GetRepository<IFormRepository>().WithKey(int.Parse(this.Request.Form["formId"]));

      foreach (Field field in this.Storage.GetRepository<IFieldRepository>().FilteredByFormId(form.Id))
      {
        string value = this.Request.Form[string.Format("field{0}", field.Id)];

        body.AppendFormat(
          "<p>{0}: {1}</p>",
          this.Storage.GetRepository<ILocalizationRepository>().FilteredByDictionaryId(field.NameId).First().Value,
          value
        );
      }

      return null;
    }
  }
}