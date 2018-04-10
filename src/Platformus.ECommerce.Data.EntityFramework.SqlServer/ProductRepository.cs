// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="IProductRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Product"/> entities in the context of SQL Server database.
  /// </summary>
  public class ProductRepository : RepositoryBase<Product>, IProductRepository
  {
    /// <summary>
    /// Gets the product by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product.</param>
    /// <returns>Found product with the given identifier.</returns>
    public Product WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the product by the URL (case insensitive).
    /// </summary>
    /// <param name="url">The URL.</param>
    /// <returns>Found product with the given URL.</returns>
    public Product WithUrl(string url)
    {
      return this.dbSet.FirstOrDefault(p => string.Equals(p.Url, url, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets the product by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the product.</param>
    /// <returns>Found product with the given code.</returns>
    public Product WithCode(string code)
    {
      return this.dbSet.FirstOrDefault(p => string.Equals(p.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets all the products using sorting by code (ascending).
    /// </summary>
    /// <returns>Found products.</returns>
    public IEnumerable<Product> All()
    {
      return this.dbSet.OrderBy(p => p.Code);
    }

    /// <summary>
    /// Gets all the products using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The product property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of products that should be skipped.</param>
    /// <param name="take">The number of products that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found products using the given filtering, sorting, and paging.</returns>
    public IEnumerable<Product> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredProducts(dbSet.AsNoTracking(), filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Gets all the products filtered by the category identifier using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="categoryId">The unique identifier of the category these products belongs to.</param>
    /// <param name="orderBy">The product property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of products that should be skipped.</param>
    /// <param name="take">The number of products that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found products filtered by the category identifier using the given filtering, sorting, and paging.</returns>
    public IEnumerable<Product> FilteredByCategoryIdRange(int categoryId, string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredProducts(dbSet.AsNoTracking(), categoryId, filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the product.
    /// </summary>
    /// <param name="product">The product to create.</param>
    public void Create(Product product)
    {
      this.dbSet.Add(product);
    }

    /// <summary>
    /// Edits the product.
    /// </summary>
    /// <param name="product">The product to edit.</param>
    public void Edit(Product product)
    {
      this.storageContext.Entry(product).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the product specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the product to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the product.
    /// </summary>
    /// <param name="product">The product to delete.</param>
    public void Delete(Product product)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          CREATE TABLE #Dictionaries (Id INT PRIMARY KEY);
          INSERT INTO #Dictionaries SELECT NameId FROM Products WHERE Id = {0};
          DELETE FROM Products WHERE Id = {0};
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM #Dictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM #Dictionaries);
        ",
        product.Id
      );
    }

    /// <summary>
    /// Counts the number of the products with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of products found.</returns>
    public int Count(string filter)
    {
      return this.GetFilteredProducts(dbSet, filter).Count();
    }

    private IQueryable<Product> GetFilteredProducts(IQueryable<Product> products, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return products;

      return products.Where(p => p.Code.ToLower().Contains(filter.ToLower()));
    }

    private IQueryable<Product> GetFilteredProducts(IQueryable<Product> products, int categoryId, string filter)
    {
      return this.GetFilteredProducts(products, filter).Where(p => p.CategoryId == categoryId);
    }
  }
}