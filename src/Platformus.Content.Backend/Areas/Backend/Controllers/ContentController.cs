// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Text;
using ExtCore.Data.Abstractions;
using Microsoft.AspNet.Mvc;
using Platformus.Content.Backend.ViewModels.Content;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;
using Platformus.Globalization.Backend.Controllers;

namespace Platformus.Content.Backend.Controllers
{
  [Area("Backend")]
  public class ContentController : ControllerBase
  {
    public ContentController(IStorage storage)
      : base(storage)
    {
    }

    public ActionResult ObjectSelectorForm(int classId, string objectIds)
    {
      return this.PartialView("_ObjectSelectorForm", new ObjectSelectorFormViewModelBuilder(this).Build(classId, objectIds));
    }

    public ActionResult GetObjectDisplayValues(string objectIds)
    {
      StringBuilder objectDisplayValues = new StringBuilder();

      if (!string.IsNullOrEmpty(objectIds))
      {
        foreach (string objectId in objectIds.Split(','))
        {
          Object @object = this.Storage.GetRepository<IObjectRepository>().WithKey(int.Parse(objectId));

          objectDisplayValues.AppendFormat(
            "<div class=\"display-value\">{0}</div>",
            string.Join(" ", new ObjectManager(this).GetDisplayProperties(@object))
          );
        }
      }

      return this.Content(objectDisplayValues.ToString());
    }
  }
}