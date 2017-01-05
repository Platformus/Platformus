// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Platformus.Barebone.ViewComponents
{
  public abstract class ViewComponentBase : ViewComponent, IRequestHandler
  {
    public IStorage Storage { get; private set; }

    public ViewComponentBase(IStorage storage)
    {
      this.Storage = storage;
    }
  }
}