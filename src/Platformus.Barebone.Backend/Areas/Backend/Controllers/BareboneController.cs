// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone.Backend.ViewModels.Barebone;

namespace Platformus.Barebone.Backend.Controllers
{
  [Area("Backend")]
  public class BareboneController : ControllerBase
  {
    public BareboneController(IStorage storage)
      : base(storage)
    {
    }

    public ActionResult DeleteForm(string targetUrl)
    {
      return this.PartialView("_DeleteForm", new DeleteFormViewModelFactory(this).Create(targetUrl));
    }

    public ActionResult ImageUploaderForm()
    {
      return this.PartialView("_ImageUploaderForm", new ImageUploaderFormViewModelFactory(this).Create());
    }
  }
}