// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.EntityFramework.PostgreSql
{
  /// <summary>
  /// Implements the <see cref="IPropertyRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Property"/> entities in the context of PostgreSQL database.
  /// </summary>
  public class PropertyRepository : RepositoryBase<Property>, IPropertyRepository
  {
    /// <summary>
    /// Gets the property by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the property.</param>
    /// <returns>Found property with the given identifier.</returns>
    public Property WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the property by the object identifier and member identifier.
    /// </summary>
    /// <param name="objectId">The unique identifier of the object this property belongs to.</param>
    /// <param name="memberId">The unique identifier of the member this property is related to.</param>
    /// <returns>Found properties with the given object identifier and member identifier.</returns>
    public Property WithObjectIdAndMemberId(int objectId, int memberId)
    {
      return this.dbSet.FirstOrDefault(p => p.ObjectId == objectId && p.MemberId == memberId);
    }

    /// <summary>
    /// Gets the properties filtered by the object identifier using sorting by identifier (ascending).
    /// </summary>
    /// <param name="objectId">The unique identifier of the object these properties belongs to.</param>
    /// <returns>Found properties.</returns>
    public IEnumerable<Property> FilteredByObjectId(int objectId)
    {
      return this.dbSet.AsNoTracking().Where(p => p.ObjectId == objectId);
    }

    /// <summary>
    /// Creates the property.
    /// </summary>
    /// <param name="property">The property to create.</param>
    public void Create(Property property)
    {
      this.dbSet.Add(property);
    }

    /// <summary>
    /// Edits the property.
    /// </summary>
    /// <param name="property">The property to edit.</param>
    public void Edit(Property property)
    {
      this.storageContext.Entry(property).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the property specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the property to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the property.
    /// </summary>
    /// <param name="property">The property to delete.</param>
    public void Delete(Property property)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          CREATE TEMP TABLE ""TempDictionaries"" (""Id"" INT PRIMARY KEY);
          INSERT INTO ""TempDictionaries"" SELECT ""StringValueId"" FROM ""Properties"" WHERE ""Id"" = {0} AND ""StringValueId"" IS NOT NULL;
          DELETE FROM ""Properties"" WHERE ""Id"" = {0};
          DELETE FROM ""Localizations"" WHERE ""DictionaryId"" IN (SELECT ""Id"" FROM ""TempDictionaries"");
          DELETE FROM ""Dictionaries"" WHERE ""Id"" IN (SELECT ""Id"" FROM ""TempDictionaries"");
        ",
        property.Id
      );
    }
  }
}