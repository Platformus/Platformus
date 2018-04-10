// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Menus.Data.Entities
{
  /// <summary>
  /// Represents a menu. The menus are used to render the menus on the frontend.
  /// They group the menu items using the unique code.
  /// </summary>
  public class Menu : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the menu.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the menu. It is set by the user and might be used for the menu retrieval.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the dictionary identifier this menu is related to. It is used to store the localizable menu name.
    /// </summary>
    public int NameId { get; set; }

    public virtual Dictionary Name { get; set; }
    public virtual ICollection<MenuItem> MenuItems { get; set; }
  }
}