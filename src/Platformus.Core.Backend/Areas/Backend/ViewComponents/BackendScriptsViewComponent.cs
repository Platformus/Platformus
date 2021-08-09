// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Backend.Metadata.Providers;

namespace Platformus.Core.Backend.ViewComponents
{
  public class BackendScriptsViewComponent : ViewComponent
  {
    private IScriptsProvider scriptsProvider;

    public BackendScriptsViewComponent(IScriptsProvider scriptsProvider)
    {
      this.scriptsProvider = scriptsProvider;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
      return this.View(this.scriptsProvider.GetScripts(this.HttpContext));
    }
  }
}