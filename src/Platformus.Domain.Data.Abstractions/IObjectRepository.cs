// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Object"/> entities.
  /// </summary>
  public interface IObjectRepository : IRepository
  {
    /// <summary>
    /// Gets the object by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the object.</param>
    /// <returns>Found object with the given identifier.</returns>
    Object WithKey(int id);

    /// <summary>
    /// Gets all the objects using sorting by identifier (ascending).
    /// </summary>
    /// <returns>Found classes.</returns>
    IEnumerable<Object> All();

    /// <summary>
    /// Gets the objects filtered by the class identifier using sorting by identifier (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the class these objects belongs to.</param>
    /// <returns>Found objects.</returns>
    IEnumerable<Object> FilteredByClassId(int classId);

    /// <summary>
    /// Creates the object.
    /// </summary>
    /// <param name="@object">The object to create.</param>
    void Create(Object @object);

    /// <summary>
    /// Edits the object.
    /// </summary>
    /// <param name="@object">The object to edit.</param>
    void Edit(Object @object);

    /// <summary>
    /// Deletes the object specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the object to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the object.
    /// </summary>
    /// <param name="@object">The object to delete.</param>
    void Delete(Object @object);
  }
}