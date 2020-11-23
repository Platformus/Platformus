// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Repositories.Abstractions;

namespace Platformus.Core.Frontend.Controllers
{
  public abstract class ControllerBase : Core.Controllers.ControllerBase
  {
    public ControllerBase(IStorage storage)
      : base(storage)
    {
    }
  }
}