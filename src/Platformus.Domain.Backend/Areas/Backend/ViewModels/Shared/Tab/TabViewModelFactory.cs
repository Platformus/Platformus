// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Shared
{
  public class TabViewModelFactory : ViewModelFactoryBase
  {
    public TabViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public TabViewModel Create(Tab tab)
    {
      return new TabViewModel()
      {
        Id = tab.Id,
        Name = tab.Name,
        Position = tab.Position
      };
    }
  }
}