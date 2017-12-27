// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Catalog"/> entities.
  /// </summary>
  public interface ICatalogRepository : IRepository
  {
    /// <summary>
    /// Gets the catalog by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the catalog.</param>
    /// <returns>Found catalog with the given identifier.</returns>
    Catalog WithKey(int id);

    /// <summary>
    /// Gets the catalogs filtered by the parent catalog identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="catalogId">The unique identifier of the parent catalog these catalogs belongs to.</param>
    /// <returns>Found catalogs.</returns>
    IEnumerable<Catalog> FilteredByCatalogId(int? catalogId);

    /// <summary>
    /// Creates the catalog.
    /// </summary>
    /// <param name="catalog">The catalog to create.</param>
    void Create(Catalog catalog);

    /// <summary>
    /// Edits the catalog.
    /// </summary>
    /// <param name="catalog">The catalog to edit.</param>
    void Edit(Catalog catalog);

    /// <summary>
    /// Deletes the catalog specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the catalog to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the catalog.
    /// </summary>
    /// <param name="catalog">The catalog to delete.</param>
    void Delete(Catalog catalog);
  }
}