// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Models.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Menus.Data.Models
{
  public class Menu : IEntity
  {
    public int Id { get; set; }
    public string Code { get; set; }
    public int NameId { get; set; }

    public virtual Dictionary Name { get; set; }
    public virtual ICollection<MenuItem> MenuItems { get; set; }
  }
}