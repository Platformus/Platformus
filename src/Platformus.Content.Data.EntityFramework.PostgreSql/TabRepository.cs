// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.PostgreSql;
using Microsoft.Data.Entity;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;

namespace Platformus.Content.Data.EntityFramework.PostgreSql
{
  public class TabRepository : RepositoryBase<Tab>, ITabRepository
  {
    public Tab WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(t => t.Id == id);
    }

    public IEnumerable<Tab> All()
    {
      return this.dbSet.OrderBy(t => t.Position);
    }

    public IEnumerable<Tab> FilteredByClassId(int classId)
    {
      return this.dbSet.Where(t => t.ClassId == classId).OrderBy(t => t.Position);
    }

    public IEnumerable<Tab> Range(int classId, string orderBy, string direction, int skip, int take)
    {
      return this.dbSet.Where(t => t.ClassId == classId).OrderBy(t => t.Position).Skip(skip).Take(take);
    }

    public void Create(Tab tab)
    {
      this.dbSet.Add(tab);
    }

    public void Edit(Tab tab)
    {
      this.dbContext.Entry(tab).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Tab tab)
    {
      this.dbContext.Database.ExecuteSqlCommand(
        @"
          UPDATE Members SET TabId = NULL WHERE TabId = {0};
        ",
        tab.Id
      );

      this.dbSet.Remove(tab);
    }

    public int CountByClassId(int classId)
    {
      return this.dbSet.Count(t => t.ClassId == classId);
    }
  }
}