// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Menus.Data.Entities;

namespace Platformus.Menus.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Menu"/> entities.
  /// </summary>
  public interface IMenuRepository : IRepository
  {
    /// <summary>
    /// Gets the menu by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the menu.</param>
    /// <returns>Found menu with the given identifier.</returns>
    Menu WithKey(int id);

    /// <summary>
    /// Gets the menu by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the menu.</param>
    /// <returns>Found menu with the given code.</returns>
    Menu WithCode(string code);

    /// <summary>
    /// Gets all the menus using sorting by code (ascending).
    /// </summary>
    /// <returns>Found menus.</returns>
    IEnumerable<Menu> All();

    /// <summary>
    /// Creates the menu.
    /// </summary>
    /// <param name="menu">The menu to create.</param>
    void Create(Menu menu);

    /// <summary>
    /// Edits the menu.
    /// </summary>
    /// <param name="menu">The menu to edit.</param>
    void Edit(Menu menu);

    /// <summary>
    /// Deletes the menu specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the menu to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the menu.
    /// </summary>
    /// <param name="menu">The menu to delete.</param>
    void Delete(Menu menu);
  }
}