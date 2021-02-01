// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;

namespace Platformus.ECommerce.Backend.ViewModels.Orders
{
  public class IndexViewModel : ViewModelBase
  {
    public GridViewModel Grid { get; set; }
  }
}