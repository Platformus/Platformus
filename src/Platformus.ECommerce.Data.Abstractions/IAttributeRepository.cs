// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Attribute"/> entities.
  /// </summary>
  public interface IAttributeRepository : IRepository
  {
    /// <summary>
    /// Gets the attribute by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the attribute.</param>
    /// <returns>Found attribute with the given identifier.</returns>
    Attribute WithKey(int id);

    /// <summary>
    /// Gets all the attributes using sorting by position (ascending).
    /// </summary>
    /// <returns>Found attributes.</returns>
    IEnumerable<Attribute> All();

    /// <summary>
    /// Gets the attributes filtered by the feature identifier using sorting by identifier (ascending).
    /// </summary>
    /// <param name="featureId">The unique identifier of the feature these attributes belongs to.</param>
    /// <returns>Found attributes.</returns>
    IEnumerable<Attribute> FilteredByFeatureId(int featureId);

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
    IEnumerable<Attribute> Range(int featureId, string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the attribute.
    /// </summary>
    /// <param name="attribute">The attribute to create.</param>
    void Create(Attribute attribute);

    /// <summary>
    /// Edits the attribute.
    /// </summary>
    /// <param name="attribute">The attribute to edit.</param>
    void Edit(Attribute attribute);

    /// <summary>
    /// Deletes the attribute specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the attribute to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the attribute.
    /// </summary>
    /// <param name="attribute">The attribute to delete.</param>
    void Delete(Attribute attribute);

    /// <summary>
    /// Counts the number of the attributes filtered by the feature identifier with the given filtering.
    /// </summary>
    /// <param name="featureId">The unique identifier of the feature these attributes belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of attributes found.</returns>
    int Count(int featureId, string filter);
  }
}