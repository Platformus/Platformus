// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Core.Backend.ViewModels;
using Platformus.Website.Backend.ViewModels.Shared;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Members;

public class IndexViewModel : ViewModelBase
{
  public MemberFilter Filter { get; set; }
  public ClassViewModel Class { get; set; }
  public string Sorting { get; set; }
  public int Offset { get; set; }
  public int Limit { get; set; }
  public int Total { get; set; }
  public IEnumerable<MemberViewModel> Members { get; set; }
}