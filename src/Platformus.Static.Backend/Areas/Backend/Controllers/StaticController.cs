// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNet.Mvc;
using Platformus.Barebone.Backend.Controllers;
using Platformus.Static.Backend.ViewModels.Static;

namespace Platformus.Static.Backend.Controllers
{
  [Area("Backend")]
  public class StaticController : ControllerBase
  {
    public StaticController(IStorage storage)
      : base(storage)
    {
    }

    public ActionResult FileSelectorForm()
    {
      return this.PartialView("_FileSelectorForm", new FileSelectorFormViewModelBuilder(this).Build());
    }
  }
}