// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace Platformus.Infrastructure
{
  public interface IBackendMetadata
  {
    IEnumerable<BackendStyleSheet> BackendStyleSheets { get; }
    IEnumerable<BackendScript> BackendScripts { get; }
    IEnumerable<BackendMenuGroup> BackendMenuGroups { get; }
    IEnumerable<BackendMenuGroup> GetBackendMenuGroups(IServiceProvider serviceProvider);
  }
}