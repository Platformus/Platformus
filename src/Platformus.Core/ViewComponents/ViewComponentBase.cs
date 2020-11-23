// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace Platformus.Core.ViewComponents
{
  public abstract class ViewComponentBase : ViewComponent
  {
    public IStorage Storage { get; private set; }

    public ViewComponentBase(IStorage storage)
    {
      this.Storage = storage;
    }
  }
}