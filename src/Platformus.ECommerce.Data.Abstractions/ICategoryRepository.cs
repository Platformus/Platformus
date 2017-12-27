// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Category"/> entities.
  /// </summary>
  public interface ICategoryRepository : IRepository
  {
    /// <summary>
    /// Gets the category by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the category.</param>
    /// <returns>Found category with the given identifier.</returns>
    Category WithKey(int id);

    /// <summary>
    /// Gets the categories filtered by the parent category identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="categoryId">The unique identifier of the parent category these categories belongs to.</param>
    /// <returns>Found categories.</returns>
    IEnumerable<Category> FilteredByCategoryId(int? categoryId);

    /// <summary>
    /// Creates the category.
    /// </summary>
    /// <param name="category">The category to create.</param>
    void Create(Category category);

    /// <summary>
    /// Edits the category.
    /// </summary>
    /// <param name="category">The category to edit.</param>
    void Edit(Category category);

    /// <summary>
    /// Deletes the category specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the category to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the category.
    /// </summary>
    /// <param name="category">The category to delete.</param>
    void Delete(Category category);
  }
}