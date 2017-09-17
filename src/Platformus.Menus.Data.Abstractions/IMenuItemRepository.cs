// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Menus.Data.Entities;

namespace Platformus.Menus.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="MenuItem"/> entities.
  /// </summary>
  public interface IMenuItemRepository : IRepository
  {
    /// <summary>
    /// Gets the menu item by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the menu item.</param>
    /// <returns>Found menu item with the given identifier.</returns>
    MenuItem WithKey(int id);

    /// <summary>
    /// Gets the menu items filtered by the menu identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="menuId">The unique identifier of the menu these menu items belongs to.</param>
    /// <returns>Found menu items.</returns>
    IEnumerable<MenuItem> FilteredByMenuId(int menuId);

    /// <summary>
    /// Gets the menu items filtered by the parent menu item identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="menuItemId">The unique identifier of the parent menu item these menu items belongs to.</param>
    /// <returns>Found menu items.</returns>
    IEnumerable<MenuItem> FilteredByMenuItemId(int menuItemId);

    /// <summary>
    /// Creates the menu item.
    /// </summary>
    /// <param name="menuItem">The menu item to create.</param>
    void Create(MenuItem menuItem);

    /// <summary>
    /// Edits the menu item.
    /// </summary>
    /// <param name="menuItem">The menu item to edit.</param>
    void Edit(MenuItem menuItem);

    /// <summary>
    /// Deletes the menu item specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the menu item to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the menu item.
    /// </summary>
    /// <param name="menuItem">The menu item to delete.</param>
    void Delete(MenuItem menuItem);
  }
}