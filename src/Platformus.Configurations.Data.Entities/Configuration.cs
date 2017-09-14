// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Configurations.Data.Entities
{
  /// <summary>
  /// Represents a configuration. The configurations are used to group the variables.
  /// </summary>
  public class Configuration : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the configuration.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the configuration. It is set by the user and might be used for the configuration retrieval.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the configuration name.
    /// </summary>
    public string Name { get; set; }

    public virtual ICollection<Variable> Variables { get; set; }
  }
}