// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Text;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Domain.Backend.ViewModels.Content;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.Backend.Controllers
{
  [Area("Backend")]
  public class ContentController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public ContentController(IStorage storage)
      : base(storage)
    {
    }

    public ActionResult ImageUploaderForm()
    {
      return this.PartialView("_ImageUploaderForm", new ImageUploaderFormViewModelFactory(this).Create());
    }

    public ActionResult ObjectSelectorForm(int classId, string objectIds)
    {
      return this.PartialView("_ObjectSelectorForm", new ObjectSelectorFormViewModelFactory(this).Create(classId, objectIds));
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
            "<div class=\"relation-editor__display-value\">{0}</div>",
            string.Join(" ", new ObjectManager(this).GetDisplayProperties(@object))
          );
        }
      }

      return this.Content(objectDisplayValues.ToString());
    }
  }
}