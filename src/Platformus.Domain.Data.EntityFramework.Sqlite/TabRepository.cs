// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.EntityFramework.Sqlite
{
  /// <summary>
  /// Implements the <see cref="ITabRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Tab"/> entities in the context of SQLite database.
  /// </summary>
  public class TabRepository : RepositoryBase<Tab>, ITabRepository
  {
    /// <summary>
    /// Gets the tab by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the tab.</param>
    /// <returns>Found tab with the given identifier.</returns>
    public Tab WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the tabs filtered by the class identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the class these tabs belongs to.</param>
    /// <returns>Found tabs.</returns>
    public IEnumerable<Tab> FilteredByClassId(int classId)
    {
      return this.dbSet.AsNoTracking().Where(t => t.ClassId == classId).OrderBy(t => t.Position);
    }

    /// <summary>
    /// Gets the tabs filtered by the class identifier (including tabs of the parent class) using sorting by position (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the class these tabs belongs to.</param>
    /// <returns>Found tabs.</returns>
    public IEnumerable<Tab> FilteredByClassIdInlcudingParent(int classId)
    {
      return this.dbSet.AsNoTracking().FromSql(
        "SELECT * FROM Tabs WHERE ClassId = {0} OR ClassId IN (SELECT ClassId FROM Classes WHERE Id = {0}) ORDER BY Position",
        classId
      );
    }

    /// <summary>
    /// Gets all the tabs filtered by the class identifier using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="classId">The unique identifier of the class these tabs belongs to.</param>
    /// <param name="orderBy">The tab property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of tabs that should be skipped.</param>
    /// <param name="take">The number of tabs that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found tabs filtered by the class identifier using the given filtering, sorting, and paging.</returns>
    public IEnumerable<Tab> FilteredByClassIdRange(int classId, string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredTabs(dbSet.AsNoTracking(), classId, filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the tab.
    /// </summary>
    /// <param name="tab">The tab to create.</param>
    public void Create(Tab tab)
    {
      this.dbSet.Add(tab);
    }

    /// <summary>
    /// Edits the tab.
    /// </summary>
    /// <param name="tab">The tab to edit.</param>
    public void Edit(Tab tab)
    {
      this.storageContext.Entry(tab).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the tab specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the tab to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the tab.
    /// </summary>
    /// <param name="tab">The tab to delete.</param>
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

    /// <summary>
    /// Counts the number of the tabs filtered by the class identifier with the given filtering.
    /// </summary>
    /// <param name="classId">The unique identifier of the class these tabs belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of tabs found.</returns>
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