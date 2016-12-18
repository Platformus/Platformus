// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class GridViewModelFactory : ViewModelFactoryBase
  {
    public GridViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public GridViewModel Create(string orderBy, string direction, int skip, int take, int total, IEnumerable<GridColumnViewModel> gridColumns, IEnumerable<ViewModelBase> items, string templateName)
    {
      GridViewModel grid = new GridViewModel()
      {
        OrderBy = orderBy,
        Direction = direction,
        Pager = new PagerViewModelFactory(this.handler).Create(skip, take, total),
        TakeSelector = new TakeSelectorViewModelFactory(this.handler).Create(take),
        Filter = new FilterViewModelFactory(this.handler).Create(null),
        GridColumns = gridColumns,
        Items = items,
        TemplateName = templateName
      };

      gridColumns.ToList().ForEach(gc => gc.Owner = grid);

      if (gridColumns.Count() != 0)
        gridColumns.ToList().Last().IsLast = true;

      return grid;
    }
  }
}