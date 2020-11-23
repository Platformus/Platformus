// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.Core.Data.Entities
{
  /// <summary>
  /// Represents a configuration. The configurations are used to group the variables.
  /// </summary>
  public class Configuration : IEntity<int>
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