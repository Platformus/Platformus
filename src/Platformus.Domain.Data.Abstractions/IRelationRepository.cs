// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Relation"/> entities.
  /// </summary>
  public interface IRelationRepository : IRepository
  {
    /// <summary>
    /// Gets the relation by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the relation.</param>
    /// <returns>Found relation with the given identifier.</returns>
    Relation WithKey(int id);

    /// <summary>
    /// Gets the relations filtered by the primary object identifier using sorting by identifier (ascending).
    /// </summary>
    /// <param name="primaryId">The unique identifier of the primary object these relations belongs to.</param>
    /// <returns>Found relations.</returns>
    IEnumerable<Relation> FilteredByPrimaryId(int primaryId);

    /// <summary>
    /// Gets the relations filtered by the foreign object identifier using sorting by identifier (ascending).
    /// </summary>
    /// <param name="foreignId">The unique identifier of the foreign object these relations belongs to.</param>
    /// <returns>Found relations.</returns>
    IEnumerable<Relation> FilteredByForeignId(int foreignId);

    /// <summary>
    /// Gets the relations filtered by the member identifier and foreign object identifier using sorting by identifier (ascending).
    /// </summary>
    /// <param name="memberId">The unique identifier of the member these relations is related to.</param>
    /// <param name="foreignId">The unique identifier of the foreign object these relations belongs to.</param>
    /// <returns>Found relations.</returns>
    IEnumerable<Relation> FilteredByMemberIdAndForeignId(int memberId, int foreignId);

    /// <summary>
    /// Creates the relation.
    /// </summary>
    /// <param name="relation">The relation to create.</param>
    void Create(Relation relation);

    /// <summary>
    /// Edits the relation.
    /// </summary>
    /// <param name="relation">The relation to edit.</param>
    void Edit(Relation relation);

    /// <summary>
    /// Deletes the relation specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the relation to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the relation.
    /// </summary>
    /// <param name="relation">The relation to delete.</param>
    void Delete(Relation relation);
  }
}