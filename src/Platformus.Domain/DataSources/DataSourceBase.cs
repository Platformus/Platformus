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
    protected IRequestHandler requestHandler;
    protected Object @object;
    protected CachedObject cachedObject;
    protected Dictionary<string, string> args;

    public void Initialize(IRequestHandler requestHandler, Object @object, params KeyValuePair<string, string>[] args)
    {
      this.requestHandler = requestHandler;
      this.@object = @object;
      this.args = args.ToDictionary(a => a.Key, a => a.Value);
    }

    public void Initialize(IRequestHandler requestHandler, CachedObject cachedObject, params KeyValuePair<string, string>[] args)
    {
      this.requestHandler = requestHandler;
      this.cachedObject = cachedObject;
      this.args = args.ToDictionary(a => a.Key, a => a.Value);
    }

    public abstract IEnumerable<Object> GetObjects();
    public abstract IEnumerable<CachedObject> GetCachedObjects();
  }
}