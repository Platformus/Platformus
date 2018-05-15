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
  /// Implements the <see cref="ISerializedProductRepository"/> interface and represents the repository
  /// for manipulating the <see cref="SerializedProduct"/> entities in the context of SQL Server database.
  /// </summary>
  public class SerializedProductRepository : RepositoryBase<SerializedProduct>, ISerializedProductRepository
  {
    /// <summary>
    /// Gets the serialized product by the culture identifier and product identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized product belongs to.</param>
    /// <param name="productId">The unique identifier of the product this serialized product belongs to.</param>
    /// <returns>Found serialized product with the given culture identifier and product identifier.</returns>
    public SerializedProduct WithKey(int cultureId, int productId)
    {
      return this.dbSet.Find(cultureId, productId);
    }

    /// <summary>
    /// Gets the serialized product by the URL (case insensitive).
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns>Found serialized product with the given URL.</returns>
    public SerializedProduct WithUrl(string url)
    {
      return this.dbSet.FirstOrDefault(sp => string.Equals(sp.Url, url, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets all the serialized products filtered by the category identifier using the given sorting and paging.
    /// </summary>
    /// <param name="categoryId">The unique identifier of the category these serialized products belongs to.</param>
    /// <param name="orderBy">The serialized product property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of serialized products that should be skipped.</param>
    /// <param name="take">The number of serialized products that should be taken.</param>
    /// <returns>Found serialized products filtered by the category identifier using the given sorting and paging.</returns>
    public IEnumerable<SerializedProduct> FilteredByCategoryIdRange(int categoryId, string orderBy, string direction, int skip, int take)
    {
      return this.dbSet.Where(sp => sp.CategoryId == categoryId).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the serialized product.
    /// </summary>
    /// <param name="serializedProduct">The serialized product to create.</param>
    public void Create(SerializedProduct serializedProduct)
    {
      this.dbSet.Add(serializedProduct);
    }

    /// <summary>
    /// Edits the serialized product.
    /// </summary>
    /// <param name="serializedProduct">The serialized product to edit.</param>
    public void Edit(SerializedProduct serializedProduct)
    {
      this.storageContext.Entry(serializedProduct).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the serialized product specified by the culture identifier and product identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized product belongs to.</param>
    /// <param name="productId">The unique identifier of the product this serialized product belongs to.</param>
    public void Delete(int cultureId, int productId)
    {
      this.Delete(this.WithKey(cultureId, productId));
    }

    /// <summary>
    /// Deletes the serialized product.
    /// </summary>
    /// <param name="serializedProduct">The serialized product to delete.</param>
    public void Delete(SerializedProduct serializedProduct)
    {
      this.dbSet.Remove(serializedProduct);
    }
  }
}