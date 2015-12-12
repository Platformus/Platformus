// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Navigation.Data.Models
{
  public class CachedMenu : IEntity
  {
    //[Key]
    //[Required]
    public int CultureId { get; set; }

    //[Key]
    //[Required]
    public int MenuId { get; set; }

    //[Required]
    //[StringLength(32)]
    public string Code { get; set; }
    public string CachedMenuItems { get; set; }

    public virtual Culture Culture { get; set; }
    public virtual Menu Menu { get; set; }
  }
}