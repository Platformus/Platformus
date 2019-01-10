// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Platformus.Barebone.Parameters
{
  public class ParameterGroup
  {
    public string Name { get; set; }
    public IEnumerable<Parameter> Parameters { get; set; }

    public ParameterGroup(string name, params Parameter[] parameters)
    {
      this.Name = name;
      this.Parameters = parameters;
    }
  }
}