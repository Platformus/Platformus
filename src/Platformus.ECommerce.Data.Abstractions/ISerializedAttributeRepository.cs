// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="SerializedAttribute"/> entities.
  /// </summary>
  public interface ISerializedAttributeRepository : IRepository
  {
    /// <summary>
    /// Gets the serialized attribute by the culture identifier and attribute identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized attribute belongs to.</param>
    /// <param name="attributeId">The unique identifier of the attribute this serialized attribute belongs to.</param>
    /// <returns>Found serialized attribute with the given culture identifier and attribute identifier.</returns>
    SerializedAttribute WithKey(int cultureId, int attributeId);

    /// <summary>
    /// Gets all the serialized attributes filtered by the culture identifier and category identifier sorting by position (ascending).
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized attributes belongs to.</param>
    /// <param name="categoryId">The unique identifier of the category these serialized attributes belongs to.</param>
    /// <returns>Found serialized attributes filtered by the culture identifier and category identifier.</returns>
    IEnumerable<SerializedAttribute> FilteredByCultureIdAndCategoryId(int cultureId, int categoryId);

    /// <summary>
    /// Creates the serialized attribute.
    /// </summary>
    /// <param name="serializedAttribute">The serialized attribute to create.</param>
    void Create(SerializedAttribute serializedAttribute);

    /// <summary>
    /// Edits the serialized attribute.
    /// </summary>
    /// <param name="serializedAttribute">The serialized attribute to edit.</param>
    void Edit(SerializedAttribute serializedAttribute);

    /// <summary>
    /// Deletes the serialized attribute specified by the culture identifier and attribute identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized attribute belongs to.</param>
    /// <param name="attributeId">The unique identifier of the attribute this serialized attribute belongs to.</param>
    void Delete(int cultureId, int attributeId);

    /// <summary>
    /// Deletes the serialized attribute.
    /// </summary>
    /// <param name="serializedAttribute">The serialized attribute to delete.</param>
    void Delete(SerializedAttribute serializedAttribute);
  }
}