// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;

namespace Platformus.Routing.DataSources
{
  public abstract class DataSourceBase : IDataSource
  {
    private Dictionary<string, string> args;

    public virtual IEnumerable<DataSourceParameterGroup> DataSourceParameterGroups => new DataSourceParameterGroup[] { };
    public virtual string Description => null;

    protected bool HasArgument(KeyValuePair<string, string>[] args, string key)
    {
      this.CacheArguments(args);
      return this.args.ContainsKey(key);
    }

    protected int GetIntArgument(KeyValuePair<string, string>[] args, string key)
    {
      this.CacheArguments(args);

      if (int.TryParse(this.args[key], out int result))
        return result;

      return 0;
    }

    protected bool GetBoolArgument(KeyValuePair<string, string>[] args, string key)
    {
      this.CacheArguments(args);

      if (bool.TryParse(this.args[key], out bool result))
        return result;

      return false;
    }

    protected string GetStringArgument(KeyValuePair<string, string>[] args, string key)
    {
      this.CacheArguments(args);
      return this.args[key];
    }

    private void CacheArguments(KeyValuePair<string, string>[] args)
    {
      if (this.args == null)
        this.args = args.ToDictionary(a => a.Key, a => a.Value);
    }
  }
}