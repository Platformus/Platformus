﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Platformus
{
  public enum CacheEntryPriority
  {
    Low,
    Normal,
    High,
    NeverRemove
  }

  public class CacheEntryOptions
  {
    public DateTimeOffset? AbsoluteExpiration { get; set; }
    public TimeSpan? SlidingExpiration { get; set; }
    public CacheEntryPriority Priority { get; set; }

    public CacheEntryOptions(DateTimeOffset? absoluteExpiration = null, TimeSpan? slidingExpiration = null, CacheEntryPriority priority = CacheEntryPriority.Normal)
    {
      this.AbsoluteExpiration = absoluteExpiration;
      this.SlidingExpiration = slidingExpiration;
      this.Priority = priority;
    }
  }

  public interface ICache
  {
    T Get<T>(string key);
    Task<T> GetWithDefaultValueAsync<T>(string key, Func<Task<T>> defaultValueFunc);
    Task<T> GetWithDefaultValueAsync<T>(string key, Func<Task<T>> defaultValueFunc, CacheEntryOptions options);
    void Set<T>(string key, T value);
    void Set<T>(string key, T value, CacheEntryOptions options);
    void Remove(string key);
    void RemoveAll();
    void RemoveAll(Func<string, bool> predicate);
  }
}