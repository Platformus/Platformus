// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Models.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Menus.Data.Models
{
  public class CachedMenu : IEntity
  {
    public int CultureId { get; set; }
    public int MenuId { get; set; }
    public string Code { get; set; }
    public string CachedMenuItems { get; set; }

    public virtual Culture Culture { get; set; }
    public virtual Menu Menu { get; set; }
  }
}