// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.FileManager.Data.Abstractions;
using Platformus.FileManager.Data.Entities;

namespace Platformus.FileManager.Data.EntityFramework.Sqlite
{
  /// <summary>
  /// Implements the <see cref="IFileRepository"/> interface and represents the repository
  /// for manipulating the <see cref="File"/> entities in the context of SQLite database.
  /// </summary>
  public class FileRepository : RepositoryBase<File>, IFileRepository
  {
    /// <summary>
    /// Gets the file by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the file.</param>
    /// <returns>Found file with the given identifier.</returns>
    public File WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets all the files using sorting by name (ascending).
    /// </summary>
    /// <returns>Found files.</returns>
    public IEnumerable<File> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(f => f.Name);
    }

    /// <summary>
    /// Gets all the files using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The file property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of files that should be skipped.</param>
    /// <param name="take">The number of files that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found files using the given filtering, sorting, and paging.</returns>
    public IEnumerable<File> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredFiles(this.dbSet.AsNoTracking(), filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the file.
    /// </summary>
    /// <param name="file">The file to create.</param>
    public void Create(File file)
    {
      this.dbSet.Add(file);
    }

    /// <summary>
    /// Edits the file.
    /// </summary>
    /// <param name="file">The file to edit.</param>
    public void Edit(File file)
    {
      this.storageContext.Entry(file).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the file specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the file to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the file.
    /// </summary>
    /// <param name="file">The file to delete.</param>
    public void Delete(File file)
    {
      this.dbSet.Remove(file);
    }

    /// <summary>
    /// Counts the number of the files with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of files found.</returns>
    public int Count(string filter)
    {
      return this.GetFilteredFiles(this.dbSet, filter).Count();
    }

    private IQueryable<File> GetFilteredFiles(IQueryable<File> files, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return files;

      return files.Where(f => f.Name.ToLower().Contains(filter.ToLower()));
    }
  }
}