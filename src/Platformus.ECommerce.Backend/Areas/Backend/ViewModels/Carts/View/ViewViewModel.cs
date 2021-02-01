// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Core.Backend.ViewModels;
using Platformus.ECommerce.Backend.ViewModels.Shared;

namespace Platformus.ECommerce.Backend.ViewModels.Carts
{
  public class ViewViewModel : ViewModelBase
  {
    public int? Id { get; set; }
    public IEnumerable<PositionViewModel> Positions { get; set; }
    public decimal Total { get; set; }
  }
}