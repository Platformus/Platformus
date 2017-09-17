// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.FileManager.Data.Entities;

namespace Platformus.FileManager.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="File"/> entities.
  /// </summary>
  public interface IFileRepository : IRepository
  {
    /// <summary>
    /// Gets the file by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the file.</param>
    /// <returns>Found file with the given identifier.</returns>
    File WithKey(int id);

    /// <summary>
    /// Gets all the files using sorting by name (ascending).
    /// </summary>
    /// <returns>Found files.</returns>
    IEnumerable<File> All();

    /// <summary>
    /// Gets all the files using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="orderBy">The file property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of files that should be skipped.</param>
    /// <param name="take">The number of files that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found files using the given filtering, sorting, and paging.</returns>
    IEnumerable<File> Range(string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the file.
    /// </summary>
    /// <param name="file">The file to create.</param>
    void Create(File file);

    /// <summary>
    /// Edits the file.
    /// </summary>
    /// <param name="file">The file to edit.</param>
    void Edit(File file);

    /// <summary>
    /// Deletes the file specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the file to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the file.
    /// </summary>
    /// <param name="file">The file to delete.</param>
    void Delete(File file);

    /// <summary>
    /// Counts the number of the files with the given filtering.
    /// </summary>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of files found.</returns>
    int Count(string filter);
  }
}