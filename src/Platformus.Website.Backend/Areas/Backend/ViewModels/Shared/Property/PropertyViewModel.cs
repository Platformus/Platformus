// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Primitives;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public class PropertyViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public MemberViewModel Member { get; set; }
    public object Value { get; set; }
    public int? IntegerValue { get; set; }
    public decimal? DecimalValue { get; set; }
    public Localization NeutralStringValue { get; set; }
    public IEnumerable<Localization> LocalizedStringValues { get; set; }
    public DateTime? DateTimeValue { get; set; }
  }
}