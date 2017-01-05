// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class GridColumnViewModelFactory : ViewModelFactoryBase
  {
    public GridColumnViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public GridColumnViewModel Create(string name, string orderBy = null)
    {
      return new GridColumnViewModel()
      {
        Name = name,
        OrderBy = string.IsNullOrEmpty(orderBy) ? null : orderBy.ToLower()
      };
    }

    public GridColumnViewModel CreateEmpty()
    {
      return new GridColumnViewModel()
      {
        Name = "&nbsp;"
      };
    }
  }
}