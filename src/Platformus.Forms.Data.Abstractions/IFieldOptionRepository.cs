// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="FieldOption"/> entities.
  /// </summary>
  public interface IFieldOptionRepository : IRepository
  {
    /// <summary>
    /// Gets the field option by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the field option.</param>
    /// <returns>Found field option with the given identifier.</returns>
    FieldOption WithKey(int id);

    /// <summary>
    /// Gets the field options filtered by the field identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="fieldId">The unique identifier of the field these field options belongs to.</param>
    /// <returns>Found field options.</returns>
    IEnumerable<FieldOption> FilteredByFieldId(int fieldId);

    /// <summary>
    /// Creates the field option.
    /// </summary>
    /// <param name="fieldOption">The field option to create.</param>
    void Create(FieldOption fieldOption);

    /// <summary>
    /// Edits the field option.
    /// </summary>
    /// <param name="fieldOption">The field option to edit.</param>
    void Edit(FieldOption fieldOption);

    /// <summary>
    /// Deletes the field option specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the field option to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the field option.
    /// </summary>
    /// <param name="fieldOption">The field option to delete.</param>
    void Delete(FieldOption fieldOption);
  }
}