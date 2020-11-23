// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Website.Filters;

namespace Platformus.Website.Backend.ViewModels.Members
{
  public class IndexViewModel : ViewModelBase
  {
    public MemberFilter Filter { get; set; }
    public GridViewModel Grid { get; set; }
  }
}