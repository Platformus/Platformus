// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone.Backend.ViewModels;
using Platformus.Barebone.Backend.ViewModels.Shared;

namespace Platformus.Routing.Backend.ViewModels.DataSources
{
  public class IndexViewModel : ViewModelBase
  {
    public int EndpointId { get; set; }
    public GridViewModel Grid { get; set; }
  }
}