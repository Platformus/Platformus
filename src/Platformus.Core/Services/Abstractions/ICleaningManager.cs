// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;

namespace Platformus.Core.Services.Abstractions
{
  /// <summary>
  /// Describes a cleaning manager that is used to clean up the unused resources (refresh tokens, saved model states etc.) on schedule.
  /// </summary>
  public interface ICleaningManager
  {
    /// <summary>
    /// Cleans up the unused resources.
    /// </summary>
    /// <param name="serviceProvider">A service provider to take the required services from.</param>
    Task CleanUpAsync(IServiceProvider serviceProvider);
  }
}