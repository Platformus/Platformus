// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;

namespace Platformus.Infrastructure
{
  public abstract class BackendMetadataBase : IBackendMetadata
  {
    public virtual IEnumerable<BackendStyleSheet> BackendStyleSheets
    {
      get
      {
        return null;
      }
    }

    public virtual IEnumerable<BackendScript> BackendScripts
    {
      get
      {
        return null;
      }
    }

    public virtual IEnumerable<BackendMenuGroup> BackendMenuGroups
    {
      get
      {
        return null;
      }
    }

    public virtual IEnumerable<BackendMenuGroup> GetBackendMenuGroups(IServiceProvider serviceProvider) => new BackendMenuGroup[] { };
  }
}