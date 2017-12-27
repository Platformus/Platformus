// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.ECommerce.Backend.ViewModels.Shared
{
  public class CategoryViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<CategoryViewModel> Categories { get; set; }
  }
}