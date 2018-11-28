// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.Domain.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="ISerializedAttributeRepository"/> interface and represents the repository
  /// for manipulating the <see cref="SerializedAttribute"/> entities in the context of SQL Server database.
  /// </summary>
  public class SerializedAttributeRepository : RepositoryBase<SerializedAttribute>, ISerializedAttributeRepository
  {
    /// <summary>
    /// Gets the serialized attribute by the culture identifier and attribute identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized attribute belongs to.</param>
    /// <param name="attributeId">The unique identifier of the attribute this serialized attribute belongs to.</param>
    /// <returns>Found serialized attribute with the given culture identifier and attribute identifier.</returns>
    public SerializedAttribute WithKey(int cultureId, int attributeId)
    {
      return this.dbSet.Find(cultureId, attributeId);
    }

    /// <summary>
    /// Gets all the serialized attributes filtered by the culture identifier and category identifier sorting by position (ascending).
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized attributes belongs to.</param>
    /// <param name="categoryId">The unique identifier of the category these serialized attributes belongs to.</param>
    /// <returns>Found serialized attributes filtered by the culture identifier and category identifier.</returns>
    public IEnumerable<SerializedAttribute> FilteredByCultureIdAndCategoryId(int cultureId, int categoryId)
    {
      return this.dbSet.FromSql(
        "SELECT * FROM SerializedAttributes WHERE CultureId = {0} AND AttributeId IN (SELECT AttributeId FROM ProductAttributes WHERE ProductId IN (SELECT Id FROM Products WHERE CategoryId = {1}))",
        cultureId, categoryId
      );
    }

    /// <summary>
    /// Creates the serialized attribute.
    /// </summary>
    /// <param name="serializedAttribute">The serialized attribute to create.</param>
    public void Create(SerializedAttribute serializedAttribute)
    {
      this.dbSet.Add(serializedAttribute);
    }

    /// <summary>
    /// Edits the serialized attribute.
    /// </summary>
    /// <param name="serializedAttribute">The serialized attribute to edit.</param>
    public void Edit(SerializedAttribute serializedAttribute)
    {
      this.storageContext.Entry(serializedAttribute).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the serialized attribute specified by the culture identifier and attribute identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized attribute belongs to.</param>
    /// <param name="attributeId">The unique identifier of the attribute this serialized attribute belongs to.</param>
    public void Delete(int cultureId, int attributeId)
    {
      this.Delete(this.WithKey(cultureId, attributeId));
    }

    /// <summary>
    /// Deletes the serialized attribute.
    /// </summary>
    /// <param name="serializedAttribute">The serialized attribute to delete.</param>
    public void Delete(SerializedAttribute serializedAttribute)
    {
      this.dbSet.Remove(serializedAttribute);
    }
  }
}