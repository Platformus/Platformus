// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.DataSources
{
  public class IndexViewModel : ViewModelBase
  {
    public DataSourceFilter Filter { get; set; }
    public GridViewModel Grid { get; set; }
  }
}