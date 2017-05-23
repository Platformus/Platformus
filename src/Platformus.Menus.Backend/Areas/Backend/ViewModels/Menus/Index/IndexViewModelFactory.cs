// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Globalization.Backend.ViewModels;
using Platformus.Menus.Backend.ViewModels.Shared;
using Platformus.Menus.Data.Abstractions;

namespace Platformus.Menus.Backend.ViewModels.Menus
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create()
    {
      return new IndexViewModel()
      {
        Menus = this.RequestHandler.Storage.GetRepository<IMenuRepository>().All().ToList().Select(
          m => new MenuViewModelFactory(this.RequestHandler).Create(m)
        )
      };
    }
  }
}