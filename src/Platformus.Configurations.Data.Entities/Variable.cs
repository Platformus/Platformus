// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Configurations.Data.Entities
{
  /// <summary>
  /// Represents a configuration variable. The variables are used to store the configuration values.
  /// </summary>
  public class Variable : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the variable.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the configuration identifier this variable belongs to.
    /// </summary>
    public int ConfigurationId { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the variable. It is set by the user and might be used for the variable retrieval.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the variable name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the variable value.
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// Gets or sets the variable position. Position is used to sort the variables inside the configuration (smallest to largest).
    /// </summary>
    public int? Position { get; set; }

    public virtual Configuration Configuration { get; set; }
  }
}