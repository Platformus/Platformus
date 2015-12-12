// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Models.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Navigation.Data.Models
{
  public class MenuItem : IEntity
  {
    //[Key]
    //[Required]
    public int Id { get; set; }
    public int? MenuId { get; set; }
    public int? MenuItemId { get; set; }

    //[Required]
    public int NameId { get; set; }

    //[StringLength(128)]
    public string Url { get; set; }
    public int? Position { get; set; }

    public virtual Menu Menu { get; set; }
    public virtual MenuItem Parent { get; set; }
    public virtual Dictionary Name { get; set; }
    public virtual ICollection<MenuItem> MenuItems { get; set; }
  }
}