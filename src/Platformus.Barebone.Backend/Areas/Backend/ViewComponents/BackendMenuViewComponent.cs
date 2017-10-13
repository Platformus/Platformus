// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Diagnostics;
using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Platformus.Barebone.Backend.ViewModels.Shared;

namespace Platformus.Barebone.Backend.ViewComponents
{
  public class BackendMenuViewComponent : ViewComponentBase
  {
      private ILoggerFactory _loggerFactory;
    public BackendMenuViewComponent(IStorage storage, ILoggerFactory loggerFactory)
      : base(storage)
    {
        _loggerFactory = loggerFactory;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        BackendMenuViewModelFactory factory = new BackendMenuViewModelFactory(this);

        Stopwatch watch = new Stopwatch();
        watch.Start();
        BackendMenuViewModel menu = await factory.CreateAsync();
        watch.Stop();
        _loggerFactory.CreateLogger<BackendMenuViewComponent>().LogInformation("Time to build menu content by BackendMenuViewModelFactory: " + watch.ElapsedMilliseconds + " ms");
        return View(menu);
    }
  }
}