// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;

namespace Platformus.Globalization.Frontend.ViewModels.Shared
{
  public class CulturesViewModelFactory : ViewModelFactoryBase
  {
    public CulturesViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public CulturesViewModel Create()
    {
      return new CulturesViewModel()
      {
        Cultures = CultureManager.GetCultures(this.handler.Storage).Where(c => !c.IsNeutral).Select(
          c => new CultureViewModelFactory(this.handler).Create(c)
        )
      };
    }
  }
}