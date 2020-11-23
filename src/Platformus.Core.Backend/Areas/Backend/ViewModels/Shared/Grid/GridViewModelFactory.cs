// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Platformus.Core.Backend.ViewModels.Shared
{
  public class GridViewModelFactory : ViewModelFactoryBase
  {
    public GridViewModel Create(HttpContext httpContext, string filteringProperty, string sorting, int skip, int take, int total, IEnumerable<GridColumnViewModel> gridColumns, IEnumerable<ViewModelBase> items, string templateName)
    {
      GridViewModel grid = new GridViewModel()
      {
        Filter = string.IsNullOrEmpty(filteringProperty) ? null : new FilterViewModelFactory().Create(httpContext, filteringProperty),
        SortingName = string.IsNullOrEmpty(sorting) ? null : this.GetSortingName(sorting),
        SortingDirection = string.IsNullOrEmpty(sorting) ? (SortingDirection?)null: this.GetSortingDirection(sorting),
        Pager = new PagerViewModelFactory().Create(skip, take, total),
        TakeSelector = new TakeSelectorViewModelFactory().Create(httpContext, take),
        GridColumns = gridColumns,
        Items = items,
        TemplateName = templateName
      };

      gridColumns.ToList().ForEach(gc => gc.Grid = grid);

      if (gridColumns.Count() != 0)
        gridColumns.ToList().Last().IsLast = true;

      return grid;
    }

    private string GetSortingName(string sorting)
    {
      return sorting.Substring(1);
    }

    private SortingDirection GetSortingDirection(string sorting)
    {
      return sorting[0] == '+' ? SortingDirection.Ascending : SortingDirection.Descending;
    }
  }
}