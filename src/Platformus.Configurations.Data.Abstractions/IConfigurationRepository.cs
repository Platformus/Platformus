// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Configurations.Data.Entities;

namespace Platformus.Configurations.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Configuration"/> entities.
  /// </summary>
  public interface IConfigurationRepository : IRepository
  {
    /// <summary>
    /// Gets the configuration by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the configuration.</param>
    /// <returns>Found configuration with the given identifier.</returns>
    Configuration WithKey(int id);

    /// <summary>
    /// Gets the configuration by the code (case insensitive).
    /// </summary>
    /// <param name="code">The unique code of the configuration.</param>
    /// <returns>Found configuration with the given code.</returns>
    Configuration WithCode(string code);

    /// <summary>
    /// Gets all the configurations using sorting by name (ascending).
    /// </summary>
    /// <returns>Found configurations.</returns>
    IEnumerable<Configuration> All();

    /// <summary>
    /// Creates the configuration.
    /// </summary>
    /// <param name="configuration">The configuration to create.</param>
    void Create(Configuration configuration);

    /// <summary>
    /// Edits the configuration.
    /// </summary>
    /// <param name="configuration">The configuration to edit.</param>
    void Edit(Configuration configuration);

    /// <summary>
    /// Deletes the configuration specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the configuration to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the configuration.
    /// </summary>
    /// <param name="configuration">The configuration to delete.</param>
    void Delete(Configuration configuration);
  }
}