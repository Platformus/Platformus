// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Tab"/> entities.
  /// </summary>
  public interface ITabRepository : IRepository
  {
    /// <summary>
    /// Gets the tab by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the tab.</param>
    /// <returns>Found tab with the given identifier.</returns>
    Tab WithKey(int id);

    /// <summary>
    /// Gets the tabs filtered by the class identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the class these tabs belongs to.</param>
    /// <returns>Found tabs.</returns>
    IEnumerable<Tab> FilteredByClassId(int classId);

    /// <summary>
    /// Gets the tabs filtered by the class identifier (including tabs of the parent class) using sorting by position (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the class these tabs belongs to.</param>
    /// <returns>Found tabs.</returns>
    IEnumerable<Tab> FilteredByClassIdInlcudingParent(int classId);

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
    IEnumerable<Tab> FilteredByClassIdRange(int classId, string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the tab.
    /// </summary>
    /// <param name="tab">The tab to create.</param>
    void Create(Tab tab);

    /// <summary>
    /// Edits the tab.
    /// </summary>
    /// <param name="tab">The tab to edit.</param>
    void Edit(Tab tab);

    /// <summary>
    /// Deletes the tab specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the tab to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the tab.
    /// </summary>
    /// <param name="tab">The tab to delete.</param>
    void Delete(Tab tab);

    /// <summary>
    /// Counts the number of the tabs filtered by the class identifier with the given filtering.
    /// </summary>
    /// <param name="classId">The unique identifier of the class these tabs belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of tabs found.</returns>
    int CountByClassId(int classId, string filter);
  }
}