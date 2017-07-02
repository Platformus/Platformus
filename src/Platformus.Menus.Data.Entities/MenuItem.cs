// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Menus.Data.Entities
{
  public class MenuItem : IEntity
  {
    public int Id { get; set; }
    public int? MenuId { get; set; }
    public int? MenuItemId { get; set; }
    public int NameId { get; set; }
    public string Url { get; set; }
    public int? Position { get; set; }

    public virtual Menu Menu { get; set; }
    public virtual MenuItem Parent { get; set; }
    public virtual Dictionary Name { get; set; }
    public virtual ICollection<MenuItem> MenuItems { get; set; }
  }
}