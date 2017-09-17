// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Configurations.Data.Entities;

namespace Platformus.Configurations.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Variable"/> entities.
  /// </summary>
  public interface IVariableRepository : IRepository
  {
    /// <summary>
    /// Gets the variable by the identifier.
    /// </summary>
    /// <param name="id">The identifier of the variable.</param>
    /// <returns>Found variable with the given identifier.</returns>
    Variable WithKey(int id);

    /// <summary>
    /// Gets the variable by the configuration identifier and code (case insensitive).
    /// </summary>
    /// <param name="configurationId">The unique identifier of the configuration this variable belongs to.</param>
    /// <param name="code">The unique code of the variable.</param>
    /// <returns>Found variable with the given configuration identifier and code.</returns>
    Variable WithConfigurationIdAndCode(int configurationId, string code);

    /// <summary>
    /// Gets the variables filtered by the configuration identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="configurationId">The unique identifier of the configuration these variables belongs to.</param>
    /// <returns>Found variables.</returns>
    IEnumerable<Variable> FilteredByConfigurationId(int configurationId);

    /// <summary>
    /// Creates the variable.
    /// </summary>
    /// <param name="configuration">The variable to create.</param>
    void Create(Variable variable);

    /// <summary>
    /// Edits the variable.
    /// </summary>
    /// <param name="variable">The variable to edit.</param>
    void Edit(Variable variable);

    /// <summary>
    /// Deletes the variable specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the variable to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the variable.
    /// </summary>
    /// <param name="variable">The variable to delete.</param>
    void Delete(Variable variable);
  }
}