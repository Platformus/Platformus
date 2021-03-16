// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Core.Frontend.ViewModels;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class PositionsViewModel : ViewModelBase
  {
    public IEnumerable<PositionViewModel> Positions { get; set; }
    public string PartialViewName { get; set; }
    public string AdditionalCssClass { get; set; }
  }
}