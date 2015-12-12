// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNet.Mvc;
using Platformus.Barebone.Backend.ViewModels.Shared;

namespace Platformus.Barebone.Backend.ViewComponents
{
  public class BackendMenuViewComponent : ViewComponentBase
  {
    public BackendMenuViewComponent(IStorage storage)
      : base(storage)
    {
    }

    public IViewComponentResult Invoke()
    {
      return this.View(new BackendMenuViewModelBuilder(null).Build());
    }
  }
}