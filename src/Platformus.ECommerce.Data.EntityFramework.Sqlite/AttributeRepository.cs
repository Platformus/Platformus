// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.EntityFramework.Sqlite
{
  /// <summary>
  /// Implements the <see cref="IAttributeRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Attribute"/> entities in the context of SQLite database.
  /// </summary>
  public class AttributeRepository : RepositoryBase<Attribute>, IAttributeRepository
  {
    /// <summary>
    /// Gets the attribute by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the attribute.</param>
    /// <returns>Found attribute with the given identifier.</returns>
    public Attribute WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets all the attributes using sorting by position (ascending).
    /// </summary>
    /// <returns>Found attributes.</returns>
    public IEnumerable<Attribute> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(a => a.Position);
    }

    /// <summary>
    /// Gets the attributes filtered by the feature identifier using sorting by identifier (ascending).
    /// </summary>
    /// <param name="featureId">The unique identifier of the feature these attributes belongs to.</param>
    /// <returns>Found attributes.</returns>
    public IEnumerable<Attribute> FilteredByFeatureId(int featureId)
    {
      return this.dbSet.AsNoTracking().Where(a => a.FeatureId == featureId).OrderBy(a => a.Position);
    }

    /// <summary>
    /// Gets the attributes filtered by the feature identifier using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="featureId">The unique identifier of the feature these attributes belongs to.</param>
    /// <param name="orderBy">The attribute property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of attributes that should be skipped.</param>
    /// <param name="take">The number of attributes that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found attributes using the given filtering, sorting, and paging.</returns>
    public IEnumerable<Attribute> Range(int featureId, string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredAttributes(dbSet.AsNoTracking(), featureId, filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the attribute.
    /// </summary>
    /// <param name="attribute">The attribute to create.</param>
    public void Create(Attribute attribute)
    {
      this.dbSet.Add(attribute);
    }

    /// <summary>
    /// Edits the attribute.
    /// </summary>
    /// <param name="attribute">The attribute to edit.</param>
    public void Edit(Attribute attribute)
    {
      this.storageContext.Entry(attribute).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the attribute specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the attribute to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the attribute.
    /// </summary>
    /// <param name="attribute">The attribute to delete.</param>
    public void Delete(Attribute attribute)
    {
      this.dbSet.Remove(attribute);
    }

    /// <summary>
    /// Counts the number of the attributes filtered by the feature identifier with the given filtering.
    /// </summary>
    /// <param name="featureId">The unique identifier of the feature these attributes belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of attributes found.</returns>
    public int Count(int featureId, string filter)
    {
      return this.GetFilteredAttributes(dbSet, featureId, filter).Count();
    }

    private IQueryable<Attribute> GetFilteredAttributes(IQueryable<Attribute> attributes, int featureId, string filter)
    {
      attributes = attributes.Where(a => a.FeatureId == featureId);

      if (string.IsNullOrEmpty(filter))
        return attributes;

      return attributes.Where(a => true);
    }
  }
}