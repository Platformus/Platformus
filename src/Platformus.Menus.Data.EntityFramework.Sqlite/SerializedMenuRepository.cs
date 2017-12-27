// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Menus.Data.Abstractions;
using Platformus.Menus.Data.Entities;

namespace Platformus.Menus.Data.EntityFramework.Sqlite
{
  /// <summary>
  /// Implements the <see cref="ISerializedMenuRepository"/> interface and represents the repository
  /// for manipulating the <see cref="SerializedMenu"/> entities in the context of SQLite database.
  /// </summary>
  public class SerializedMenuRepository : RepositoryBase<SerializedMenu>, ISerializedMenuRepository
  {
    /// <summary>
    /// Gets the serialized menu by the culture identifier and menu identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized menu belongs to.</param>
    /// <param name="menuId">The unique identifier of the menu this serialized menu belongs to.</param>
    /// <returns>Found serialized menu with the given culture identifier and menu identifier.</returns>
    public SerializedMenu WithKey(int cultureId, int menuId)
    {
      return this.dbSet.Find(cultureId, menuId);
    }

    /// <summary>
    /// Gets the serialized menu by the culture identifier and menu code (case insensitive).
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized menu belongs to.</param>
    /// <param name="code">The unique code of the menu this serialized menu belongs to.</param>
    /// <returns>Found serialized menu with the given culture identifier and menu code.</returns>
    public SerializedMenu WithCultureIdAndCode(int cultureId, string code)
    {
      return this.dbSet.FirstOrDefault(sm => sm.CultureId == cultureId && string.Equals(sm.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Creates the serialized menu.
    /// </summary>
    /// <param name="serializedMenu">The serialized menu to create.</param>
    public void Create(SerializedMenu serializedMenu)
    {
      this.dbSet.Add(serializedMenu);
    }

    /// <summary>
    /// Edits the serialized menu.
    /// </summary>
    /// <param name="serializedMenu">The serialized menu to edit.</param>
    public void Edit(SerializedMenu serializedMenu)
    {
      this.storageContext.Entry(serializedMenu).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the serialized menu specified by the culture identifier and menu identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized menu belongs to.</param>
    /// <param name="menuId">The unique identifier of the menu this serialized menu belongs to.</param>
    public void Delete(int cultureId, int menuId)
    {
      this.Delete(this.WithKey(cultureId, menuId));
    }

    /// <summary>
    /// Deletes the serialized menu.
    /// </summary>
    /// <param name="serializedMenu">The serialized menu to delete.</param>
    public void Delete(SerializedMenu serializedMenu)
    {
      this.dbSet.Remove(serializedMenu);
    }
  }
}