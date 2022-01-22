// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Backend.ViewModels.Core;
using Platformus.Core.Backend.ViewModels.Shared;

namespace Platformus.Core.Backend.Controllers
{
  public class CoreController : ControllerBase
  {
    public CoreController(IStorage storage)
      : base(storage)
    {
    }

    public IActionResult ParameterEditor(string cSharpClassName)
    {
      return this.PartialView("_ParameterEditor", ParameterEditorViewModelFactory.Create(cSharpClassName));
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