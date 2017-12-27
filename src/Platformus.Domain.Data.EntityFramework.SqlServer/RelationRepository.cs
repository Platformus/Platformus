// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="IRelationRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Relation"/> entities in the context of SQL Server database.
  /// </summary>
  public class RelationRepository : RepositoryBase<Relation>, IRelationRepository
  {
    /// <summary>
    /// Gets the relation by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the relation.</param>
    /// <returns>Found relation with the given identifier.</returns>
    public Relation WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the relations filtered by the primary object identifier using sorting by identifier (ascending).
    /// </summary>
    /// <param name="primaryId">The unique identifier of the primary object these relations belongs to.</param>
    /// <returns>Found relations.</returns>
    public IEnumerable<Relation> FilteredByPrimaryId(int primaryId)
    {
      return this.dbSet.AsNoTracking().Where(r => r.PrimaryId == primaryId);
    }

    /// <summary>
    /// Gets the relations filtered by the foreign object identifier using sorting by identifier (ascending).
    /// </summary>
    /// <param name="foreignId">The unique identifier of the foreign object these relations belongs to.</param>
    /// <returns>Found relations.</returns>
    public IEnumerable<Relation> FilteredByForeignId(int foreignId)
    {
      return this.dbSet.AsNoTracking().Where(r => r.ForeignId == foreignId);
    }

    /// <summary>
    /// Gets the relations filtered by the member identifier and foreign object identifier using sorting by identifier (ascending).
    /// </summary>
    /// <param name="memberId">The unique identifier of the member these relations is related to.</param>
    /// <param name="foreignId">The unique identifier of the foreign object these relations belongs to.</param>
    /// <returns>Found relations.</returns>
    public IEnumerable<Relation> FilteredByMemberIdAndForeignId(int memberId, int foreignId)
    {
      return this.dbSet.AsNoTracking().Where(r => r.MemberId == memberId && r.ForeignId == foreignId);
    }

    /// <summary>
    /// Creates the relation.
    /// </summary>
    /// <param name="relation">The relation to create.</param>
    public void Create(Relation relation)
    {
      this.dbSet.Add(relation);
    }

    /// <summary>
    /// Edits the relation.
    /// </summary>
    /// <param name="relation">The relation to edit.</param>
    public void Edit(Relation relation)
    {
      this.storageContext.Entry(relation).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the relation specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the relation to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the relation.
    /// </summary>
    /// <param name="relation">The relation to delete.</param>
    public void Delete(Relation relation)
    {
      this.dbSet.Remove(relation);
    }
  }
}