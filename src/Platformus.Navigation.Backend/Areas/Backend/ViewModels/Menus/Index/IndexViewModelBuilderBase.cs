// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Globalization.Backend.ViewModels;
using Platformus.Navigation.Backend.ViewModels.Shared;
using Platformus.Navigation.Data.Abstractions;

namespace Platformus.Navigation.Backend.ViewModels.Menus
{
  public class IndexViewModelBuilder : ViewModelBuilderBase
  {
    public IndexViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public IndexViewModel Build()
    {
      return new IndexViewModel()
      {
        Menus = this.handler.Storage.GetRepository<IMenuRepository>().All().Select(
          m => new MenuViewModelBuilder(this.handler).Build(m)
        )
      };
    }
  }
}