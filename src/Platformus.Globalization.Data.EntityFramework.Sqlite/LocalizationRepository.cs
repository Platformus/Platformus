// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.Sqlite;
using Microsoft.Data.Entity;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Globalization.Data.EntityFramework.Sqlite
{
  public class LocalizationRepository : RepositoryBase<Localization>, ILocalizationRepository
  {
    public Localization WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(l => l.Id == id);
    }

    public Localization WithDictionaryIdAndCultureId(int dictionaryId, int cultureId)
    {
      return this.dbSet.FirstOrDefault(l => l.DictionaryId == dictionaryId && l.CultureId == cultureId);
    }

    public IEnumerable<Localization> FilteredByDictionaryId(int dictionaryId)
    {
      return this.dbSet.Where(l => l.DictionaryId == dictionaryId);
    }

    public void Create(Localization localization)
    {
      this.dbSet.Add(localization);
    }

    public void Edit(Localization localization)
    {
      this.dbContext.Entry(localization).State = EntityState.Modified;
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