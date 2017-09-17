// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="SerializedObject"/> entities.
  /// </summary>
  public interface ISerializedObjectRepository : IRepository
  {
    /// <summary>
    /// Gets the serialized object by the culture identifier and object identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized object belongs to.</param>
    /// <param name="objectId">The unique identifier of the object this serialized object belongs to.</param>
    /// <returns>Found serialized object with the given culture identifier and object identifier.</returns>
    SerializedObject WithKey(int cultureId, int objectId);

    /// <summary>
    /// Gets the serialized object by the culture identifier and URL object property value (case insensitive).
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized object belongs to.</param>
    /// <param name="urlPropertyStringValue">The URL object property value.</param>
    /// <returns>Found serialized object with the given culture identifier and URL object property value.</returns>
    SerializedObject WithCultureIdAndUrlPropertyStringValue(int cultureId, string urlPropertyStringValue);

    /// <summary>
    /// Gets the serialized objects filtered by the culture identifier and class identifier
    /// using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="classId">The unique identifier of the class these serialized objects belongs to.</param>
    /// <param name="@params">The filtering, sorting, and paging parameters.</param>
    /// <returns>Found serialized objects.</returns>
    IEnumerable<SerializedObject> FilteredByCultureIdAndClassId(int cultureId, int classId, Params @params);

    /// <summary>
    /// Gets the serialized objects filtered by the culture identifier, class identifier, and object identifier
    /// using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="classId">The unique identifier of the class these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <param name="@params">The filtering, sorting, and paging parameters.</param>
    /// <returns>Found serialized objects.</returns>
    IEnumerable<SerializedObject> FilteredByCultureIdAndClassIdAndObjectId(int cultureId, int classId, int objectId, Params @params);

    /// <summary>
    /// Gets the primary serialized objects filtered by the culture identifier and object identifier
    /// using sorting by identifier (ascending).
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <returns>Found primary serialized objects.</returns>
    IEnumerable<SerializedObject> Primary(int cultureId, int objectId);

    /// <summary>
    /// Gets the primary serialized objects filtered by the culture identifier and object identifier
    /// using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <param name="@params">The filtering, sorting, and paging parameters.</param>
    /// <returns>Found primary serialized objects.</returns>
    IEnumerable<SerializedObject> Primary(int cultureId, int objectId, Params @params);

    /// <summary>
    /// Gets the primary serialized objects filtered by the culture identifier, member identifier, and object identifier
    /// using sorting by identifier (ascending).
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="memberId">The unique identifier of the member these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <returns>Found primary serialized objects.</returns>
    IEnumerable<SerializedObject> Primary(int cultureId, int memberId, int objectId);

    /// <summary>
    /// Gets the primary serialized objects filtered by the culture identifier, member identifier, and object identifier
    /// using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="memberId">The unique identifier of the member these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <param name="@params">The filtering, sorting, and paging parameters.</param>
    /// <returns>Found primary serialized objects.</returns>
    IEnumerable<SerializedObject> Primary(int cultureId, int memberId, int objectId, Params @params);

    /// <summary>
    /// Gets the foreign serialized objects filtered by the culture identifier and object identifier
    /// using sorting by identifier (ascending).
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <returns>Found foreign serialized objects.</returns>
    IEnumerable<SerializedObject> Foreign(int cultureId, int objectId);

    /// <summary>
    /// Gets the foreign serialized objects filtered by the culture identifier and object identifier
    /// using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <param name="@params">The filtering, sorting, and paging parameters.</param>
    /// <returns>Found foreign serialized objects.</returns>
    IEnumerable<SerializedObject> Foreign(int cultureId, int objectId, Params @params);

    /// <summary>
    /// Gets the foreign serialized objects filtered by the culture identifier, member identifier, and object identifier
    /// using sorting by identifier (ascending).
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="memberId">The unique identifier of the member these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <returns>Found foreign serialized objects.</returns>
    IEnumerable<SerializedObject> Foreign(int cultureId, int memberId, int objectId);

    /// <summary>
    /// Gets the foreign serialized objects filtered by the culture identifier, member identifier, and object identifier
    /// using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="memberId">The unique identifier of the member these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <param name="@params">The filtering, sorting, and paging parameters.</param>
    /// <returns>Found foreign serialized objects.</returns>
    IEnumerable<SerializedObject> Foreign(int cultureId, int memberId, int objectId, Params @params);

    /// <summary>
    /// Creates the serialized object.
    /// </summary>
    /// <param name="serializedObject">The serialized object to create.</param>
    void Create(SerializedObject serializedObject);

    /// <summary>
    /// Edits the serialized object.
    /// </summary>
    /// <param name="serializedObject">The serialized object to edit.</param>
    void Edit(SerializedObject serializedObject);

    /// <summary>
    /// Deletes the serialized object specified by the culture identifier and object identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized object belongs to.</param>
    /// <param name="objectId">The unique identifier of the object this serialized object belongs to.</param>
    void Delete(int cultureId, int objectId);

    /// <summary>
    /// Deletes the serialized object.
    /// </summary>
    /// <param name="serializedObject">The serialized object to delete.</param>
    void Delete(SerializedObject serializedObject);

    /// <summary>
    /// Counts the number of the serialized objects filtered by the culture identifier and class identifier with the given filtering.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="classId">The unique identifier of the class these serialized objects belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of serialized objects found.</returns>
    int CountByCultureIdAndClassId(int cultureId, int classId, Params @params);

    /// <summary>
    /// Counts the number of the serialized objects filtered by the culture identifier, class identifier, and object identifier
    /// with the given filtering.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="classId">The unique identifier of the class these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of serialized objects found.</returns>
    int CountByCultureIdAndClassIdAndObjectId(int cultureId, int classId, int objectId, Params @params);
  }
}