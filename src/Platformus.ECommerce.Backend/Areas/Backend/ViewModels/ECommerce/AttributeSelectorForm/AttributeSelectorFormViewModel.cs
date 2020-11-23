﻿// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.Core.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.ECommerce
{
  public class AttributeSelectorFormViewModel : ViewModelBase
  {
    public IEnumerable<GridColumnViewModel> GridColumns { get; set; }
    public IEnumerable<AttributeViewModel> Attributes { get; set; }
    public int? AttributeId { get; set; }
  }
}