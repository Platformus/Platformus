// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.ECommerce.Backend.ViewModels.Shared;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Catalogs
{
  public class IndexViewModel : ViewModelBase
  {
    public IEnumerable<CatalogViewModel> Catalogs { get; set; }
  }
}