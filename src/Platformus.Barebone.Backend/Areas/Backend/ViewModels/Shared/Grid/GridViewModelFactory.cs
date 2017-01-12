// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class GridViewModelFactory : ViewModelFactoryBase
  {
    public GridViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public GridViewModel Create(string orderBy, string direction, int skip, int take, int total, IEnumerable<GridColumnViewModel> gridColumns, IEnumerable<ViewModelBase> items, string templateName)
    {
      GridViewModel grid = new GridViewModel()
      {
        OrderBy = orderBy,
        Direction = direction,
        Pager = new PagerViewModelFactory(this.RequestHandler).Create(skip, take, total),
        TakeSelector = new TakeSelectorViewModelFactory(this.RequestHandler).Create(take),
        // TODO: replace with the parameter
        Filter = new FilterViewModelFactory(this.RequestHandler).Create(this.RequestHandler.HttpContext.Request.Query["filter"]),
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