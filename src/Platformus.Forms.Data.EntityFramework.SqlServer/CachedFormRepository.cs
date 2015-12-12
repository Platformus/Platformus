// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using ExtCore.Data.EntityFramework.SqlServer;
using Microsoft.Data.Entity;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Models;

namespace Platformus.Forms.Data.EntityFramework.SqlServer
{
  public class CachedFormRepository : RepositoryBase<CachedForm>, ICachedFormRepository
  {
    public CachedForm WithKey(int cultureId, int formId)
    {
      return this.dbSet.FirstOrDefault(cf => cf.CultureId == cultureId && cf.FormId == formId);
    }

    public CachedForm WithCultureIdAndCode(int cultureId, string code)
    {
      return this.dbSet.FirstOrDefault(cf => cf.CultureId == cultureId && string.Equals(cf.Code, code, System.StringComparison.OrdinalIgnoreCase));
    }

    public void Create(CachedForm cachedForm)
    {
      this.dbSet.Add(cachedForm);
    }

    public void Edit(CachedForm cachedForm)
    {
      this.dbContext.Entry(cachedForm).State = EntityState.Modified;
    }

    public void Delete(int cultureId, int formId)
    {
      this.Delete(this.WithKey(cultureId, formId));
    }

    public void Delete(CachedForm cachedForm)
    {
      this.dbSet.Remove(cachedForm);
    }
  }
}