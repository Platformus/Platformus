// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Repositories.Abstractions;

namespace Platformus.Core.Backend.ViewComponents
{
  public abstract class ViewComponentBase : Core.ViewComponents.ViewComponentBase
  {
    public ViewComponentBase(IStorage storage)
      : base(storage)
    {
    }
  }
}