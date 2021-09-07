// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Core.Backend;
using Platformus.Core.Backend.ViewModels;
using Platformus.Website.Backend.ViewModels.Shared;

namespace Platformus.Website.Backend.ViewModels.Website
{
  public class MemberSelectorFormViewModel : ViewModelBase
  {
    public IEnumerable<TableTagHelper.Column> TableColumns { get; set; }
    public IDictionary<ClassViewModel, IEnumerable<MemberViewModel>> MembersByClasses { get; set; }
    public int? MemberId { get; set; }
  }
}