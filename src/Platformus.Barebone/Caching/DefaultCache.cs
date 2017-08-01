// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;

namespace Platformus
{
  public class DefaultCache : ICache
  {
    private IMemoryCache memoryCache;
    private IList<string> keys;

    public DefaultCache(IMemoryCache memoryCache)
    {
      this.memoryCache = memoryCache;
      this.keys = new List<string>();
    }

    public T Get<T>(string key)
    {
      return this.memoryCache.Get<T>(key);
    }

    public T GetWithDefaultValue<T>(string key, Func<T> defaultValueFunc)
    {
      T result = this.Get<T>(key);

      if (result == null)
      {
        T defaultValue = defaultValueFunc();

        this.Set(key, defaultValue);
        return defaultValue;
      }

      return result;
    }

    public void Set<T>(string key, T value)
    {
      this.memoryCache.Set(key, value);
      this.keys.Add(key);
    }

    public void Remove(string key)
    {
      this.memoryCache.Remove(key);
      this.keys.Remove(key);
    }

    public void RemoveAll()
    {
      foreach (string key in this.keys)
        this.memoryCache.Remove(key);

      this.keys.Clear();
    }
  }
}