// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Globalization.Data.EntityFramework.PostgreSql
{
  /// <summary>
  /// Implements the <see cref="ILocalizationRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Localization"/> entities in the context of PostgreSQL database.
  /// </summary>
  public class LocalizationRepository : RepositoryBase<Localization>, ILocalizationRepository
  {
    public Localization WithKey(int id)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(l => l.Id == id);
    }

    public Localization WithDictionaryIdAndCultureId(int dictionaryId, int cultureId)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(l => l.DictionaryId == dictionaryId && l.CultureId == cultureId);
    }

    public IEnumerable<Localization> FilteredByDictionaryId(int dictionaryId)
    {
      return this.dbSet.AsNoTracking().Where(l => l.DictionaryId == dictionaryId);
    }

    public void Create(Localization localization)
    {
      this.dbSet.Add(localization);
    }

    public void Edit(Localization localization)
    {
      this.storageContext.Entry(localization).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Localization localization)
    {
      this.dbSet.Remove(localization);
    }
  }
}