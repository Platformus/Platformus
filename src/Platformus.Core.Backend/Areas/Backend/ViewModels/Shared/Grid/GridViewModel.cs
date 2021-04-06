// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Platformus.Core.Backend.ViewModels.Shared
{
  public enum SortingDirection
  {
    Ascending,
    Descending
  }

  public class GridViewModel : ViewModelBase
  {
    public IEnumerable<FilterViewModel> Filters { get; set; }
    public string SortingName { get; set; }
    public SortingDirection? SortingDirection { get; set; }
    public PagerViewModel Pager { get; set; }
    public TakeSelectorViewModel TakeSelector { get; set; }
    public IEnumerable<GridColumnViewModel> GridColumns { get; set; }
    public IEnumerable<ViewModelBase> Items { get; set; }
    public string TemplateName { get; set; }
  }
}