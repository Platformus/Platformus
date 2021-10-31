// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Platformus.Core.Parameters
{
  /// <summary>
  /// Groups the <see cref="Parameter"/> and adds a group title on the client-side.
  /// </summary>
  public class ParameterGroup
  {
    /// <summary>
    /// A group name. It is displayed on the client-side as the group title.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Parameters of a group.
    /// </summary>
    public IEnumerable<Parameter> Parameters { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ParameterGroup"/> class.
    /// </summary>
    /// <param name="name">A group name. It is displayed on the client-side as the group title.</param>
    /// <param name="parameters">Parameters of a group.</param>
    public ParameterGroup(string name, params Parameter[] parameters)
    {
      this.Name = name;
      this.Parameters = parameters;
    }
  }
}