// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Property"/> entities.
  /// </summary>
  public interface IPropertyRepository : IRepository
  {
    /// <summary>
    /// Gets the property by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the property.</param>
    /// <returns>Found property with the given identifier.</returns>
    Property WithKey(int id);

    /// <summary>
    /// Gets the property by the object identifier and member identifier.
    /// </summary>
    /// <param name="objectId">The unique identifier of the object this property belongs to.</param>
    /// <param name="memberId">The unique identifier of the member this property is related to.</param>
    /// <returns>Found properties with the given object identifier and member identifier.</returns>
    Property WithObjectIdAndMemberId(int objectId, int memberId);

    /// <summary>
    /// Gets the properties filtered by the object identifier using sorting by identifier (ascending).
    /// </summary>
    /// <param name="objectId">The unique identifier of the object these properties belongs to.</param>
    /// <returns>Found properties.</returns>
    IEnumerable<Property> FilteredByObjectId(int objectId);

    /// <summary>
    /// Creates the property.
    /// </summary>
    /// <param name="property">The property to create.</param>
    void Create(Property property);

    /// <summary>
    /// Edits the property.
    /// </summary>
    /// <param name="property">The property to edit.</param>
    void Edit(Property property);

    /// <summary>
    /// Deletes the property specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the property to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the property.
    /// </summary>
    /// <param name="property">The property to delete.</param>
    void Delete(Property property);
  }
}