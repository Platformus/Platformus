// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Backend.ViewModels.Core;

namespace Platformus.Core.Backend.Controllers
{
  [Area("Backend")]
  public class CoreController : ControllerBase
  {
    public CoreController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult DeleteForm(string targetUrl)
    {
      return this.PartialView("_DeleteForm", DeleteFormViewModelFactory.Create(targetUrl));
    }

    public IActionResult ImageUploaderForm()
    {
      return this.PartialView("_ImageUploaderForm", ImageUploaderFormViewModelFactory.Create());
    }
  }
}