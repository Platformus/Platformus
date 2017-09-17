// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Platformus.Menus.Data.Entities;

namespace Platformus.Menus.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="SerializedMenu"/> entities.
  /// </summary>
  public interface ISerializedMenuRepository : IRepository
  {
    /// <summary>
    /// Gets the serialized menu by the culture identifier and menu identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized menu belongs to.</param>
    /// <param name="menuId">The unique identifier of the menu this serialized menu belongs to.</param>
    /// <returns>Found serialized menu with the given culture identifier and menu identifier.</returns>
    SerializedMenu WithKey(int cultureId, int menuId);

    /// <summary>
    /// Gets the serialized menu by the culture identifier and menu code (case insensitive).
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized menu belongs to.</param>
    /// <param name="code">The unique code of the menu this serialized menu belongs to.</param>
    /// <returns>Found serialized menu with the given culture identifier and menu code.</returns>
    SerializedMenu WithCultureIdAndCode(int cultureId, string code);

    /// <summary>
    /// Creates the serialized menu.
    /// </summary>
    /// <param name="serializedMenu">The serialized menu to create.</param>
    void Create(SerializedMenu serializedMenu);

    /// <summary>
    /// Edits the serialized menu.
    /// </summary>
    /// <param name="serializedMenu">The serialized menu to edit.</param>
    void Edit(SerializedMenu serializedMenu);

    /// <summary>
    /// Deletes the serialized menu specified by the culture identifier and menu identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized menu belongs to.</param>
    /// <param name="menuId">The unique identifier of the menu this serialized menu belongs to.</param>
    void Delete(int cultureId, int menuId);

    /// <summary>
    /// Deletes the serialized menu.
    /// </summary>
    /// <param name="serializedMenu">The serialized menu to delete.</param>
    void Delete(SerializedMenu serializedMenu);
  }
}