// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.SqlServer;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Globalization.Data.EntityFramework.SqlServer
{
  public class CultureRepository : RepositoryBase<Culture>, ICultureRepository
  {
    public Culture WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(c => c.Id == id);
    }

    public Culture WithCode(string code)
    {
      return this.dbSet.FirstOrDefault(c => string.Equals(c.Code, code, System.StringComparison.OrdinalIgnoreCase));
    }

    public Culture Neutral()
    {
      return this.dbSet.FirstOrDefault(c => c.IsNeutral);
    }

    public Culture Default()
    {
      return this.dbSet.FirstOrDefault(c => c.IsDefault);
    }

    public IEnumerable<Culture> All()
    {
      return this.dbSet.OrderBy(c => c.Code);
    }

    public IEnumerable<Culture> NotNeutral()
    {
      return this.dbSet.Where(c => !c.IsNeutral).OrderBy(c => c.Name);
    }

    public IEnumerable<Culture> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredCultures(dbSet, filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    public void Create(Culture culture)
    {
      this.dbSet.Add(culture);
    }

    public void Edit(Culture culture)
    {
      this.storageContext.Entry(culture).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Culture culture)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM SerializedObjects WHERE CultureId = {0};
          DELETE FROM CachedMenus WHERE CultureId = {0};
          DELETE FROM CachedForms WHERE CultureId = {0};
          DELETE FROM Localizations WHERE CultureId = {0};
        ",
        culture.Id
      );

      this.dbSet.Remove(culture);
    }

    public int Count(string filter)
    {
      return this.GetFilteredCultures(dbSet, filter).Count();
    }

    private IQueryable<Culture> GetFilteredCultures(IQueryable<Culture> cultures, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return cultures;

      return cultures.Where(c => c.Name.ToLower().Contains(filter.ToLower()));
    }
  }
}