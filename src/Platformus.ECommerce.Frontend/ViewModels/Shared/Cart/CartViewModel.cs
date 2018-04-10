// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class CartViewModel : ViewModelBase
  {
    public IEnumerable<PositionViewModel> Positions { get; set; }
    public string AdditionalCssClass { get; set; }
  }
}