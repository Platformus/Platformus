// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class GridViewModelBuilder : ViewModelBuilderBase
  {
    public GridViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public GridViewModel Build(string orderBy, string direction, int skip, int take, int total, IEnumerable<GridColumnViewModel> gridColumns, IEnumerable<ViewModelBase> items, string templateName)
    {
      GridViewModel grid = new GridViewModel()
      {
        OrderBy = orderBy,
        Direction = direction,
        Pager = new PagerViewModelBuilder(this.handler).Build(skip, take, total),
        TakeSelector = new TakeSelectorViewModelBuilder(this.handler).Build(take),
        Filter = new FilterViewModelBuilder(this.handler).Build(null),
        GridColumns = gridColumns,
        Items = items,
        TemplateName = templateName
      };

      gridColumns.ToList().ForEach(gc => gc.Owner = grid);
      return grid;
    }
  }
}