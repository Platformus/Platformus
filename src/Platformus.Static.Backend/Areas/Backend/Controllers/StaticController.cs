// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Static.Backend.ViewModels.Static;

namespace Platformus.Static.Backend.Controllers
{
  [Area("Backend")]
  public class StaticController : Barebone.Backend.Controllers.ControllerBase
  {
    public StaticController(IStorage storage)
      : base(storage)
    {
    }

    public ActionResult FileSelectorForm()
    {
      return this.PartialView("_FileSelectorForm", new FileSelectorFormViewModelFactory(this).Create());
    }
  }
}