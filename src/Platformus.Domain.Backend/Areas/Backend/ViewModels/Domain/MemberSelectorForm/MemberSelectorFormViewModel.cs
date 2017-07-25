// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Domain.Backend.ViewModels.Shared;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Domain
{
  public class MemberSelectorFormViewModel : ViewModelBase
  {
    public IEnumerable<GridColumnViewModel> GridColumns { get; set; }
    public IDictionary<ClassViewModel, IEnumerable<MemberViewModel>> MembersByClasses { get; set; }
    public int? MemberId { get; set; }
  }
}