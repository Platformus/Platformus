// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public class TabViewModelFactory : ViewModelFactoryBase
  {
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