﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Core.Frontend.ViewModels;
using Platformus.ECommerce.Frontend.ViewModels.Shared;

namespace Platformus.ECommerce.Frontend.ViewModels.ECommerce
{
  public class CategoryPageViewModel : ViewModelBase
  {
    public string Name { get; set; }
    public IEnumerable<ProductViewModel> Products { get; set; }
  }
}