// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="FieldType"/> entities.
  /// </summary>
  public interface IFieldTypeRepository : IRepository
  {
    /// <summary>
    /// Gets the field type by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the field type.</param>
    /// <returns>Found field type with the given identifier.</returns>
    FieldType WithKey(int id);

    /// <summary>
    /// Gets all the field types using sorting by position (ascending).
    /// </summary>
    /// <returns>Found field types.</returns>
    IEnumerable<FieldType> All();
  }
}