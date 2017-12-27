// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Configurations.Data.Abstractions;
using Platformus.Configurations.Data.Entities;

namespace Platformus.Configurations.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="IConfigurationRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Configuration"/> entities in the context of SQL Servere database.
  /// </summary>
  public class ConfigurationRepository : RepositoryBase<Configuration>, IConfigurationRepository
  {
    /// <summary>
    /// Gets the configuration by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the configuration.</param>
    /// <returns>Found configuration with the given identifier.</returns>
    public Configuration WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the configuration by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the configuration.</param>
    /// <returns>Found configuration with the given code.</returns>
    public Configuration WithCode(string code)
    {
      return this.dbSet.FirstOrDefault(c => string.Equals(c.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets all the configurations using sorting by name (ascending).
    /// </summary>
    /// <returns>Found configurations.</returns>
    public IEnumerable<Configuration> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(c => c.Name);
    }

    /// <summary>
    /// Creates the configuration.
    /// </summary>
    /// <param name="configuration">The configuration to create.</param>
    public void Create(Configuration configuration)
    {
      this.dbSet.Add(configuration);
    }

    /// <summary>
    /// Edits the configuration.
    /// </summary>
    /// <param name="configuration">The configuration to edit.</param>
    public void Edit(Configuration configuration)
    {
      this.storageContext.Entry(configuration).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the configuration specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the configuration to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the configuration.
    /// </summary>
    /// <param name="configuration">The configuration to delete.</param>
    public void Delete(Configuration configuration)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM Variables WHERE ConfigurationId = {0};
        ",
        configuration.Id
      );

      this.dbSet.Remove(configuration);
    }
  }
}