﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Platformus;

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

  public async Task<T> GetWithDefaultValueAsync<T>(string key, Func<Task<T>> defaultValueFuncAsync)
  {
    return await this.GetWithDefaultValueAsync<T>(key, defaultValueFuncAsync, null);
  }

  public async Task<T> GetWithDefaultValueAsync<T>(string key, Func<Task<T>> defaultValueFuncAsync, CacheEntryOptions options)
  {
    T result = this.Get<T>(key);

    if (result == null)
    {
      T defaultValue = await defaultValueFuncAsync();

      this.Set(key, defaultValue, options);
      return defaultValue;
    }

    return result;
  }

  public void Set<T>(string key, T value)
  {
    this.Set<T>(key, value, null);
  }

  public void Set<T>(string key, T value, CacheEntryOptions options)
  {
    if (options == null)
      this.memoryCache.Set(key, value);

    else this.memoryCache.Set(
      key,
      value,
      new MemoryCacheEntryOptions()
      {
        AbsoluteExpiration = options.AbsoluteExpiration,
        SlidingExpiration = options.SlidingExpiration,
        Priority = (CacheItemPriority)options.Priority
      }
    );

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

  public void RemoveAll(Func<string, bool> predicate)
  {
    IList<string> removedKeys = new List<string>();

    foreach (string key in this.keys)
    {
      if (predicate(key))
      {
        this.memoryCache.Remove(key);
        removedKeys.Add(key);
      }
    }

    foreach (string removedKey in removedKeys)
      this.keys.Remove(removedKey);
  }
}