// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Configurations.Data.Abstractions;
using Platformus.Configurations.Data.Entities;

namespace Platformus.Configurations.Data.EntityFramework.Sqlite
{
  /// <summary>
  /// Implements the <see cref="IVariableRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Variable"/> entities in the context of SQLite database.
  /// </summary>
  public class VariableRepository : RepositoryBase<Variable>, IVariableRepository
  {
    /// <summary>
    /// Gets the variable by the identifier.
    /// </summary>
    /// <param name="id">The identifier of the variable.</param>
    /// <returns>Found variable with the given identifier.</returns>
    public Variable WithKey(int id)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(v => v.Id == id);
    }

    /// <summary>
    /// Gets the variable by the configuration identifier and code (case insensitive).
    /// </summary>
    /// <param name="configurationId">The unique identifier of the configuration this variable belongs to.</param>
    /// <param name="code">The unique code of the variable.</param>
    /// <returns>Found variable with the given configuration identifier and code.</returns>
    public Variable WithConfigurationIdAndCode(int configurationId, string code)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(v => v.ConfigurationId == configurationId && string.Equals(v.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets the variables filtered by the configuration identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="configurationId">The unique identifier of the configuration these variables belongs to.</param>
    /// <returns>Found variables.</returns>
    public IEnumerable<Variable> FilteredByConfigurationId(int configurationId)
    {
      return this.dbSet.AsNoTracking().Where(v => v.ConfigurationId == configurationId).OrderBy(v => v.Position);
    }

    /// <summary>
    /// Creates the variable.
    /// </summary>
    /// <param name="configuration">The variable to create.</param>
    public void Create(Variable variable)
    {
      this.dbSet.Add(variable);
    }

    /// <summary>
    /// Edits the variable.
    /// </summary>
    /// <param name="variable">The variable to edit.</param>
    public void Edit(Variable variable)
    {
      this.storageContext.Entry(variable).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the variable specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the variable to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the variable.
    /// </summary>
    /// <param name="variable">The variable to delete.</param>
    public void Delete(Variable variable)
    {
      this.dbSet.Remove(variable);
    }
  }
}