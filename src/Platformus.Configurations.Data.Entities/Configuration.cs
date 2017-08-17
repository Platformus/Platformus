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
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Variable> Variables { get; set; }
  }
}