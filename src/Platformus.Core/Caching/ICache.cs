// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Platformus
{
  /// <summary>
  /// Describes a cache that can be used to store some frequently used data.
  /// </summary>
  public interface ICache
  {
    /// <summary>
    /// Gets an object from the cache by the given key.
    /// </summary>
    /// <typeparam name="T">A type the cached object should be cast to.</typeparam>
    /// <param name="key">A string identifying the entry.</param>
    T Get<T>(string key);

    /// <summary>
    /// Gets an object from the cache by the given key.
    /// If the corresponding cache entry is empty, the <paramref name="defaultValueFunc"/> function
    /// will be executed, then its result cached and returned.
    /// </summary>
    /// <typeparam name="T">A type the cached object should be cast to.</typeparam>
    /// <param name="key">A string identifying the entry.</param>
    /// <param name="defaultValueFunc">A function that should be executed if the cache entry is empty.</param>
    Task<T> GetWithDefaultValueAsync<T>(string key, Func<Task<T>> defaultValueFunc);

    /// <summary>
    /// Gets an object from the cache by the given key.
    /// If the corresponding cache entry is empty, the <paramref name="defaultValueFunc"/> function
    /// will be executed, then its result cached and returned.
    /// </summary>
    /// <typeparam name="T">A type the cached object should be cast to.</typeparam>
    /// <param name="key">A string identifying the entry.</param>
    /// <param name="defaultValueFunc">A function that should be executed if the cache entry is empty.</param>
    /// <param name="options">The options that should be applied to a created cache entry
    /// if the <paramref name="defaultValueFunc"/> function is executed.</param>
    Task<T> GetWithDefaultValueAsync<T>(string key, Func<Task<T>> defaultValueFunc, CacheEntryOptions options);

    /// <summary>
    /// Sets an object to the cache with the given key.
    /// </summary>
    /// <param name="key">A string identifying the entry.</param>
    /// <param name="value">An object to cache.</param>
    void Set<T>(string key, T value);

    /// <summary>
    /// Sets an object to the cache with the given key.
    /// </summary>
    /// <param name="key">A string identifying the entry.</param>
    /// <param name="value">An object to cache.</param>
    /// <param name="options">The options that should be applied to a created cache entry.</param>
    void Set<T>(string key, T value, CacheEntryOptions options);

    /// <summary>
    /// Removes a cache entry with the given key.
    /// </summary>
    /// <param name="key">A string identifying the entry.</param>
    void Remove(string key);

    /// <summary>
    /// Removes all the cache entries.
    /// </summary>
    void RemoveAll();

    /// <summary>
    /// Removes all the cache entries that match the given predicate.
    /// </summary>
    /// <param name="predicate">A predicate to filter the entries by keys that should be removed.</param>
    void RemoveAll(Func<string, bool> predicate);
  }
}