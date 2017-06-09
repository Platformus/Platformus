// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Platformus.Barebone.Backend.ViewModels.Shared;

namespace Platformus.Barebone.Backend.ViewComponents
{
  public class BackendScriptsViewComponent : ViewComponentBase
  {
    public BackendScriptsViewComponent(IStorage storage)
      : base(storage)
    {
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
      return this.View(new BackendScriptsViewModelFactory(this).Create());
    }
  }
}