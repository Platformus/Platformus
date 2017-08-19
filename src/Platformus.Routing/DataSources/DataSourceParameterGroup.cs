// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Platformus.Routing.DataSources
{
  public class DataSourceParameterGroup
  {
    public string Name { get; set; }
    public IEnumerable<DataSourceParameter> Parameters { get; set; }

    public DataSourceParameterGroup(string name, params DataSourceParameter[] parameters)
    {
      this.Name = name;
      this.Parameters = parameters;
    }
  }
}