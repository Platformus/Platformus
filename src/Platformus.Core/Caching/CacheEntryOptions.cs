// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Platformus;

/// <summary>
/// Describes a cache entry options (such as expiration and priority).
/// </summary>
public class CacheEntryOptions
{
  /// <summary>
  /// Gets or sets an absolute expiration date and time for the cache entry.
  /// </summary>
  public DateTimeOffset? AbsoluteExpiration { get; set; }

  /// <summary>
  /// Gets or sets an expiration time span relative to now.
  /// </summary>
  public TimeSpan? SlidingExpiration { get; set; }

  /// <summary>
  /// Gets or sets the entry priority.
  /// Entries with lower priority more likely to be removed if resources are insufficient.
  /// Default priority is <see cref="CacheEntryPriority.Normal"/>
  /// </summary>
  public CacheEntryPriority Priority { get; set; }

  /// <summary>
  /// Initializes a new instance of the <see cref="CacheEntryOptions"/> class.
  /// </summary>
  /// <param name="absoluteExpiration">An absolute expiration date and time for the cache entry.</param>
  /// <param name="slidingExpiration">An expiration time span relative to now.</param>
  /// <param name="priority">The entry priority. Entries with lower priority more likely to be removed if resources are insufficient.</param>
  public CacheEntryOptions(DateTimeOffset? absoluteExpiration = null, TimeSpan? slidingExpiration = null, CacheEntryPriority priority = CacheEntryPriority.Normal)
  {
    this.AbsoluteExpiration = absoluteExpiration;
    this.SlidingExpiration = slidingExpiration;
    this.Priority = priority;
  }
}