// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;

namespace Platformus.Barebone.Frontend.ViewComponents
{
  public abstract class ViewComponentBase : Platformus.Barebone.ViewComponents.ViewComponentBase
  {
    public ViewComponentBase(IStorage storage)
      : base(storage)
    {
    }
  }
}