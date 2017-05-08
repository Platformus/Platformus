// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Platformus.Barebone.Backend;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Shared
{
  public class PropertyViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public int? IntegerValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public IEnumerable<Localization> StringValueLocalizations { get; set; }
    public DateTime? DateTimeValue { get; set; }
  }
}