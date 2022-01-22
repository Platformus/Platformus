// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Core.Backend.ViewModels;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public class ObjectViewModel : ViewModelBase
  {
    public int Id { get; set; }
    public IEnumerable<PropertyViewModel> Properties { get; set; }
    public IEnumerable<MemberViewModel> RelationSingleParentMembers { get; set; }
  }
}