// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using ExtCore.Data.EntityFramework.SqlServer;
using Microsoft.EntityFrameworkCore;
using Platformus.Navigation.Data.Abstractions;
using Platformus.Navigation.Data.Models;

namespace Platformus.Navigation.Data.EntityFramework.SqlServer
{
  public class CachedMenuRepository : RepositoryBase<CachedMenu>, ICachedMenuRepository
  {
    public CachedMenu WithKey(int cultureId, int menuId)
    {
      return this.dbSet.FirstOrDefault(cm => cm.CultureId == cultureId && cm.MenuId == menuId);
    }

    public CachedMenu WithCultureIdAndCode(int cultureId, string code)
    {
      return this.dbSet.FirstOrDefault(cm => cm.CultureId == cultureId && string.Equals(cm.Code, code, System.StringComparison.OrdinalIgnoreCase));
    }

    public void Create(CachedMenu cachedMenu)
    {
      this.dbSet.Add(cachedMenu);
    }

    public void Edit(CachedMenu cachedMenu)
    {
      this.dbContext.Entry(cachedMenu).State = EntityState.Modified;
    }

    public void Delete(int cultureId, int menuId)
    {
      this.Delete(this.WithKey(cultureId, menuId));
    }

    public void Delete(CachedMenu cachedMenu)
    {
      this.dbSet.Remove(cachedMenu);
    }
  }
}