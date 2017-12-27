// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Globalization.Data.EntityFramework.Sqlite
{
  /// <summary>
  /// Implements the <see cref="ILocalizationRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Localization"/> entities in the context of SQLite database.
  /// </summary>
  public class LocalizationRepository : RepositoryBase<Localization>, ILocalizationRepository
  {
    /// <summary>
    /// Gets the localization by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the localization.</param>
    /// <returns>Found localization with the given identifier.</returns>
    public Localization WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the localization by the dictionary identifier and culture identifier.
    /// </summary>
    /// <param name="dictionaryId">The unique identifier of the dictionary this localization belongs to.</param>
    /// <param name="cultureId">The unique identifier of the culture this localization belongs to.</param>
    /// <returns>Found localization with the given dictionary identifier and culture identifier.</returns>
    public Localization WithDictionaryIdAndCultureId(int dictionaryId, int cultureId)
    {
      return this.dbSet.FirstOrDefault(l => l.DictionaryId == dictionaryId && l.CultureId == cultureId);
    }

    /// <summary>
    /// Gets the localizations filtered by the dictionary identifier using sorting by identifier (ascending).
    /// </summary>
    /// <param name="dictionaryId">The unique identifier of the dictionary these localizations belongs to.</param>
    /// <returns>Found objects.</returns>
    public IEnumerable<Localization> FilteredByDictionaryId(int dictionaryId)
    {
      return this.dbSet.AsNoTracking().Where(l => l.DictionaryId == dictionaryId);
    }

    /// <summary>
    /// Creates the localization.
    /// </summary>
    /// <param name="localization">The localization to create.</param>
    public void Create(Localization localization)
    {
      this.dbSet.Add(localization);
    }

    /// <summary>
    /// Edits the localization.
    /// </summary>
    /// <param name="localization">The localization to edit.</param>
    public void Edit(Localization localization)
    {
      this.storageContext.Entry(localization).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the localization specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the localization to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the localization.
    /// </summary>
    /// <param name="localization">The localization to delete.</param>
    public void Delete(Localization localization)
    {
      this.dbSet.Remove(localization);
    }
  }
}