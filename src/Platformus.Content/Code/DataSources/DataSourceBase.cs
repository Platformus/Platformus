// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Content.Data.Models;

namespace Platformus.Content.DataSources
{
  public abstract class DataSourceBase : IDataSource
  {
    protected IHandler handler;
    protected Object @object;
    protected CachedObject cachedObject;
    protected Dictionary<string, string> args;

    public void Initialize(IHandler handler, Object @object, params KeyValuePair<string, string>[] args)
    {
      this.handler = handler;
      this.@object = @object;
      this.args = args.ToDictionary(a => a.Key, a => a.Value);
    }

    public void Initialize(IHandler handler, CachedObject cachedObject, params KeyValuePair<string, string>[] args)
    {
      this.handler = handler;
      this.cachedObject = cachedObject;
      this.args = args.ToDictionary(a => a.Key, a => a.Value);
    }

    public abstract IEnumerable<Object> GetObjects();
    public abstract IEnumerable<CachedObject> GetCachedObjects();
  }
}