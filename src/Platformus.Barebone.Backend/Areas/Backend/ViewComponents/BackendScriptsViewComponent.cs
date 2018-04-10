// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone.Backend.Metadata.Providers;

namespace Platformus.Barebone.Backend.ViewComponents
{
  public class BackendScriptsViewComponent : ViewComponentBase
  {
    private IScriptsProvider scriptsProvider;

    public BackendScriptsViewComponent(IStorage storage, IScriptsProvider scriptsProvider)
      : base(storage)
    {
      this.scriptsProvider = scriptsProvider;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
      return this.View(this.scriptsProvider.GetScripts(this));
    }
  }
}