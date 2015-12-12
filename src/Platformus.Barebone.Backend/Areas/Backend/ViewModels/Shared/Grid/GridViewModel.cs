// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class GridViewModel : ViewModelBase
  {
    public string OrderBy { get; set; }
    public string Direction { get; set; }
    public PagerViewModel Pager { get; set; }
    public TakeSelectorViewModel TakeSelector { get; set; }
    public FilterViewModel Filter { get; set; }
    public IEnumerable<GridColumnViewModel> GridColumns { get; set; }
    public IEnumerable<ViewModelBase> Items { get; set; }
    public string TemplateName { get; set; }
  }
}