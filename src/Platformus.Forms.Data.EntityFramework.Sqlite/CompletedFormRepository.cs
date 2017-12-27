// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.Data.EntityFramework.Sqlite
{
  /// <summary>
  /// Implements the <see cref="ICompletedFormRepository"/> interface and represents the repository
  /// for manipulating the <see cref="CompletedForm"/> entities in the context of SQLite database.
  /// </summary>
  public class CompletedFormRepository : RepositoryBase<CompletedForm>, ICompletedFormRepository
  {
    /// <summary>
    /// Gets the completed form by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the completed form.</param>
    /// <returns>Found completed form with the given identifier.</returns>
    public CompletedForm WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets all the completed forms filtered by the form identifier using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="formId">The unique identifier of the form these completed forms belongs to.</param>
    /// <param name="orderBy">The completed form property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of completed forms that should be skipped.</param>
    /// <param name="take">The number of completed forms that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found completed forms filtered by the form identifier using the given filtering, sorting, and paging.</returns>
    public IEnumerable<CompletedForm> Range(int formId, string orderBy, string direction, int skip, int take)
    {
      return this.dbSet.AsNoTracking().Where(cf => cf.FormId == formId).OrderByDescending(cf => cf.Created).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the completed form.
    /// </summary>
    /// <param name="completedForm">The completed form to create.</param>
    public void Create(CompletedForm completedForm)
    {
      this.dbSet.Add(completedForm);
    }

    /// <summary>
    /// Deletes the completed form specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the completed form to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the completed form.
    /// </summary>
    /// <param name="completedForm">The completed form to delete.</param>
    public void Delete(CompletedForm completedForm)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM CompletedFields WHERE CompletedFormId = {0};
        ",
        completedForm.Id
      );

      this.dbSet.Remove(completedForm);
    }

    /// <summary>
    /// Counts the number of the completed forms filtered by the form identifier with the given filtering.
    /// </summary>
    /// <param name="formId">The unique identifier of the form these completed forms belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of completed forms found.</returns>
    public int Count(int formId)
    {
      return this.dbSet.Count(cf => cf.FormId == formId);
    }
  }
}