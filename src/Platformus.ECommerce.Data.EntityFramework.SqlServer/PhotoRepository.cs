﻿// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Core.Data.Extensions;
using Platformus.ECommerce.Data.Abstractions;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="IPhotoRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Photo"/> entities in the context of SQL Server database.
  /// </summary>
  public class PhotoRepository : RepositoryBase<Photo>, IPhotoRepository
  {
    /// <summary>
    /// Gets the photo by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the photo.</param>
    /// <returns>Found photo with the given identifier.</returns>
    public Photo WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the cover photo filtered by the product identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product this photo belongs to.</param>
    /// <returns>Found cover photo with the given product identifier.</returns>
    public Photo CoverByProductId(int productId)
    {
      return this.dbSet.FirstOrDefault(ph => ph.ProductId == productId && ph.IsCover);
    }

    /// <summary>
    /// Gets the photos filtered by the product identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="productId">The unique identifier of the product these photos belongs to.</param>
    /// <returns>Found photos.</returns>
    public IEnumerable<Photo> FilteredByProductId(int productId)
    {
      return this.dbSet.AsNoTracking().Where(ph => ph.ProductId == productId).OrderBy(ph => ph.Position);
    }

    /// <summary>
    /// Creates the photo.
    /// </summary>
    /// <param name="photo">The photo to create.</param>
    public void Create(Photo photo)
    {
      this.dbSet.Add(photo);
    }

    /// <summary>
    /// Edits the photo.
    /// </summary>
    /// <param name="photo">The photo to edit.</param>
    public void Edit(Photo photo)
    {
      this.storageContext.Entry(photo).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the photo specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the photo to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the photo.
    /// </summary>
    /// <param name="photo">The photo to delete.</param>
    public void Delete(Photo photo)
    {
      this.dbSet.Remove(photo);
    }

    /// <summary>
    /// Counts the number of the photos filtered by the product identifier with the given filtering.
    /// </summary>
    /// <param name="productId">The unique identifier of the product these photos belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of photos found.</returns>
    public int Count(int productId, string filter)
    {
      return this.GetFilteredPhotos(dbSet, productId, filter).Count();
    }

    private IQueryable<Photo> GetFilteredPhotos(IQueryable<Photo> photos, int productId, string filter)
    {
      photos = photos.Where(ph => ph.ProductId == productId);

      if (string.IsNullOrEmpty(filter))
        return photos;

      return photos.Where(ph => true);
    }
  }
}