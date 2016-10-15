// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Globalization.Backend.ViewModels;
using Platformus.Navigation.Backend.ViewModels.Shared;
using Platformus.Navigation.Data.Abstractions;

namespace Platformus.Navigation.Backend.ViewModels.Menus
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public IndexViewModel Create()
    {
      return new IndexViewModel()
      {
        Menus = this.handler.Storage.GetRepository<IMenuRepository>().All().Select(
          m => new MenuViewModelFactory(this.handler).Create(m)
        )
      };
    }
  }
}