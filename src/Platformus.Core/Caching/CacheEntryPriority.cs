// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus;

/// <summary>
/// Describes possible cache entry priority levels.
/// Entries with lower priority more likely to be removed if resources are insufficient.
/// </summary>
public enum CacheEntryPriority
{
  Low,
  Normal,
  High,
  NeverRemove
}