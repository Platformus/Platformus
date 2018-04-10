// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Globalization.Frontend.ViewModels;

namespace Platformus.ECommerce.Frontend.ViewModels.Shared
{
  public class CatalogViewModel : ViewModelBase
  {
    public string Url { get; set; }
    public string Name { get; set; }
    public IEnumerable<CatalogViewModel> Catalogs { get; set; }
    public int Level { get; set; }
  }
}