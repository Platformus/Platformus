// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Menus.Data.Entities
{
  /// <summary>
  /// Represents a menu item. The menu items are used to build the menus.
  /// </summary>
  public class MenuItem : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the menu item.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the menu identifier this menu item belongs to.
    /// </summary>
    public int? MenuId { get; set; }

    /// <summary>
    /// Gets or sets the menu item identifier this menu item belongs to (as a parent menu item).
    /// </summary>
    public int? MenuItemId { get; set; }

    /// <summary>
    /// Gets or sets the dictionary identifier this menu item is related to. It is used to store the localizable menu item name.
    /// </summary>
    public int NameId { get; set; }

    /// <summary>
    /// Gets or sets the menu item URL.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    /// Gets or sets the menu item position. Position is used to sort the menu items inside the menu (smallest to largest).
    /// </summary>
    public int? Position { get; set; }

    public virtual Menu Menu { get; set; }
    public virtual MenuItem Parent { get; set; }
    public virtual Dictionary Name { get; set; }
    public virtual ICollection<MenuItem> MenuItems { get; set; }
  }
}