// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="ITabRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Tab"/> entities in the context of SQL Server database.
  /// </summary>
  public class TabRepository : RepositoryBase<Tab>, ITabRepository
  {
    public Tab WithKey(int id)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(t => t.Id == id);
    }

    public IEnumerable<Tab> FilteredByClassId(int classId)
    {
      return this.dbSet.AsNoTracking().Where(t => t.ClassId == classId).OrderBy(t => t.Position);
    }

    public IEnumerable<Tab> FilteredByClassIdInlcudingParent(int classId)
    {
      return this.dbSet.AsNoTracking().FromSql(
        "SELECT * FROM Tabs WHERE ClassId = {0} OR ClassId IN (SELECT ClassId FROM Classes WHERE Id = {0}) ORDER BY Position",
        classId
      );
    }

    public IEnumerable<Tab> FilteredByClassIdRange(int classId, string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredTabs(dbSet.AsNoTracking(), classId, filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    public void Create(Tab tab)
    {
      this.dbSet.Add(tab);
    }

    public void Edit(Tab tab)
    {
      this.storageContext.Entry(tab).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Tab tab)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          UPDATE Members SET TabId = NULL WHERE TabId = {0};
        ",
        tab.Id
      );

      this.dbSet.Remove(tab);
    }

    public int CountByClassId(int classId, string filter)
    {
      return this.GetFilteredTabs(dbSet, classId, filter).Count();
    }

    private IQueryable<Tab> GetFilteredTabs(IQueryable<Tab> tabs, int classId, string filter)
    {
      tabs = tabs.Where(t => t.ClassId == classId);

      if (string.IsNullOrEmpty(filter))
        return tabs;

      return tabs.Where(t => t.Name.ToLower().Contains(filter.ToLower()));
    }
  }
}