// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Platformus.Core.Backend.ViewModels.Shared
{
  public static class GridViewModelFactory
  {
    public static GridViewModel Create(HttpContext httpContext, string sorting, int skip, int take, int total, IEnumerable<GridColumnViewModel> gridColumns, IEnumerable<ViewModelBase> items, string templateName)
    {
      return Create(httpContext, default(FilterViewModel), sorting, skip, take, total, gridColumns, items, templateName);
    }

    public static GridViewModel Create(HttpContext httpContext, FilterViewModel filter, string sorting, int skip, int take, int total, IEnumerable<GridColumnViewModel> gridColumns, IEnumerable<ViewModelBase> items, string templateName)
    {
      return Create(httpContext, filter == null ? null : new[] { filter }, sorting, skip, take, total, gridColumns, items, templateName);
    }

    public static GridViewModel Create(HttpContext httpContext, IEnumerable<FilterViewModel> filters, string sorting, int skip, int take, int total, IEnumerable<GridColumnViewModel> gridColumns, IEnumerable<ViewModelBase> items, string templateName)
    {
      GridViewModel grid = new GridViewModel()
      {
        Filters = filters,
        SortingName = string.IsNullOrEmpty(sorting) ? null : GetSortingName(sorting),
        SortingDirection = string.IsNullOrEmpty(sorting) ? null: GetSortingDirection(sorting),
        Pager = PagerViewModelFactory.Create(skip, take, total),
        TakeSelector = TakeSelectorViewModelFactory.Create(httpContext, take),
        GridColumns = gridColumns,
        Items = items,
        TemplateName = templateName
      };

      gridColumns.ToList().ForEach(gc => gc.Grid = grid);
      return grid;
    }

    private static string GetSortingName(string sorting)
    {
      return sorting.Substring(1);
    }

    private static SortingDirection GetSortingDirection(string sorting)
    {
      return sorting[0] == '+' ? SortingDirection.Ascending : SortingDirection.Descending;
    }
  }
}