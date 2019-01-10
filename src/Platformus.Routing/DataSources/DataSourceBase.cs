// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Parameters;
using Platformus.Routing.Data.Entities;

namespace Platformus.Routing.DataSources
{
  public abstract class DataSourceBase : IDataSource
  {
    protected IRequestHandler requestHandler;
    protected DataSource dataSource;
    private Dictionary<string, string> parameterValuesByCodes;

    public virtual IEnumerable<ParameterGroup> ParameterGroups => new ParameterGroup[] { };
    public virtual string Description => null;

    public dynamic GetData(IRequestHandler requestHandler, DataSource dataSource)
    {
      this.requestHandler = requestHandler;
      this.dataSource = dataSource;
      return this.GetData();
    }

    protected abstract dynamic GetData();

    protected bool HasParameter(string key)
    {
      this.CacheParameterValuesByCodes();
      return this.parameterValuesByCodes.ContainsKey(key);
    }

    protected int GetIntParameterValue(string key)
    {
      this.CacheParameterValuesByCodes();

      if (int.TryParse(this.parameterValuesByCodes[key], out int result))
        return result;

      return 0;
    }

    protected bool GetBoolParameterValue(string key)
    {
      this.CacheParameterValuesByCodes();

      if (bool.TryParse(this.parameterValuesByCodes[key], out bool result))
        return result;

      return false;
    }

    protected string GetStringParameterValue(string key)
    {
      this.CacheParameterValuesByCodes();
      return this.parameterValuesByCodes[key];
    }

    private void CacheParameterValuesByCodes()
    {
      if (this.parameterValuesByCodes == null)
        this.parameterValuesByCodes = ParametersParser.Parse(this.dataSource.Parameters).ToDictionary(a => a.Key, a => a.Value);
    }
  }
}