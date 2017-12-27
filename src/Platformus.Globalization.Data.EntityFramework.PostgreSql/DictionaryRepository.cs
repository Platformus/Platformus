// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Globalization.Data.EntityFramework.PostgreSql
{
  /// <summary>
  /// Implements the <see cref="IDictionaryRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Dictionary"/> entities in the context of PostgreSQL database.
  /// </summary>
  public class DictionaryRepository : RepositoryBase<Dictionary>, IDictionaryRepository
  {
    /// <summary>
    /// Gets the dictionary by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the dictionary.</param>
    /// <returns>Found dictionary with the given identifier.</returns>
    public Dictionary WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Creates the dictionary.
    /// </summary>
    /// <param name="dictionary">The dictionary to create.</param>
    public void Create(Dictionary dictionary)
    {
      this.dbSet.Add(dictionary);
    }

    /// <summary>
    /// Edits the dictionary.
    /// </summary>
    /// <param name="dictionary">The dictionary to edit.</param>
    public void Edit(Dictionary dictionary)
    {
      this.storageContext.Entry(dictionary).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the dictionary specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the dictionary to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the dictionary.
    /// </summary>
    /// <param name="dictionary">The dictionary to delete.</param>
    public void Delete(Dictionary dictionary)
    {
      this.dbSet.Remove(dictionary);
    }
  }
}