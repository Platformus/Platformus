// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Field"/> entities.
  /// </summary>
  public interface IFieldRepository : IRepository
  {
    /// <summary>
    /// Gets the field by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the field.</param>
    /// <returns>Found field with the given identifier.</returns>
    Field WithKey(int id);

    /// <summary>
    /// Gets the field by the form identifier and code (case insensitive).
    /// </summary>
    /// <param name="formId">The unique identifier of the form this field belongs to.</param>
    /// <param name="code">The unique code of the field.</param>
    /// <returns>Found field with the given form identifier and code.</returns>
    Field WithFormIdAndCode(int formId, string code);

    /// <summary>
    /// Gets the fields filtered by the form identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="formId">The unique identifier of the form these fields belongs to.</param>
    /// <returns>Found fields.</returns>
    IEnumerable<Field> FilteredByFormId(int formId);

    /// <summary>
    /// Creates the field.
    /// </summary>
    /// <param name="field">The field to create.</param>
    void Create(Field field);

    /// <summary>
    /// Edits the field.
    /// </summary>
    /// <param name="field">The field to edit.</param>
    void Edit(Field field);

    /// <summary>
    /// Deletes the field specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the field to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the field.
    /// </summary>
    /// <param name="field">The field to delete.</param>
    void Delete(Field field);
  }
}