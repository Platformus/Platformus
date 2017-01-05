// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Platformus.Domain.Backend.ViewModels.Shared;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Objects
{
  public class CreateOrEditViewModel : ViewModelBase
  {
    public int? Id { get; set; }
    public int? ClassId { get; set; }

    [Display(Name = "View name")]
    [StringLength(32)]
    public string ViewName { get; set; }

    [Display(Name = "URL")]
    [StringLength(128)]
    public string _Url { get; set; }

    public ClassViewModel Class { get; set; }
    public IDictionary<TabViewModel, IEnumerable<MemberViewModel>> MembersByTabs { get; set; }
  }
}