// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend.ViewModels.Users
{
  public class IndexViewModel : ViewModelBase
  {
    public IEnumerable<Option> RoleOptions { get; set; }
    public string Sorting { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
    public int Total { get; set; }
    public IEnumerable<UserViewModel> Users { get; set; }
  }
}