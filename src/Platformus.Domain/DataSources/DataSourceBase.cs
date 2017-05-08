// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.DataSources
{
  public abstract class DataSourceBase : IDataSource
  {
    private Dictionary<string, string> args;

    public abstract IEnumerable<SerializedObject> GetSerializedObjects(IRequestHandler requestHandler, SerializedObject serializedPage, params KeyValuePair<string, string>[] args);
    public abstract IEnumerable<Object> GetObjects(IRequestHandler requestHandler, Object page, params KeyValuePair<string, string>[] args);

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