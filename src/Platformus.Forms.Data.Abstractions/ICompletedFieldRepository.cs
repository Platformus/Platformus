// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="CompletedField"/> entities.
  /// </summary>
  public interface ICompletedFieldRepository : IRepository
  {
    /// <summary>
    /// Gets the completed fields filtered by the completed form identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="completedFormId">The unique identifier of the completed form these completed fields belongs to.</param>
    /// <returns>Found completed fields.</returns>
    IEnumerable<CompletedField> FilteredByCompletedFormId(int completedFormId);

    /// <summary>
    /// Creates the completed field.
    /// </summary>
    /// <param name="completedField">The completed field to create.</param>
    void Create(CompletedField completedField);
  }
}