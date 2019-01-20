// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ExtCore.Data.Abstractions;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Entities;
using Platformus.Globalization.Services.Abstractions;

namespace Platformus.Globalization.Services.Defaults
{
  public class DefaultCultureManager : ICultureManager
  {
    private ICache cache;
    private IStorage storage;

    public DefaultCultureManager(ICache cache, IStorage storage)
    {
      this.cache = cache;
      this.storage = storage;
    }

    public Culture GetCulture(int id)
    {
      return this.GetCachedCultures().FirstOrDefault(c => c.Id == id);
    }

    public Culture GetCulture(string code)
    {
      return this.GetCachedCultures().FirstOrDefault(c => string.Equals(c.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    public Culture GetNeutralCulture()
    {
      return this.GetCachedCultures().FirstOrDefault(c => c.IsNeutral);
    }

    public Culture GetFrontendDefaultCulture()
    {
      return this.GetCachedCultures().FirstOrDefault(c => c.IsFrontendDefault);
    }

    public Culture GetBackendDefaultCulture()
    {
      return this.GetCachedCultures().FirstOrDefault(c => c.IsBackendDefault);
    }

    public Culture GetCurrentCulture()
    {
      Culture currentCulture = this.GetCachedCultures().FirstOrDefault(
        c => string.Equals(c.Code, CultureInfo.CurrentCulture.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase)
      );

      if (currentCulture == null)
        currentCulture = this.GetCachedCultures().FirstOrDefault(
          c => string.Equals(c.Code, DefaultCulture.Code, StringComparison.OrdinalIgnoreCase)
        );

      return currentCulture;
    }

    public IEnumerable<Culture> GetCultures()
    {
      return this.GetCachedCultures();
    }

    public IEnumerable<Culture> GetNotNeutralCultures()
    {
      return this.GetCachedCultures().Where(c => !c.IsNeutral);
    }

    public void InvalidateCache()
    {
      this.cache.Remove("cultures");
    }

    private IEnumerable<Culture> GetCachedCultures()
    {
      return this.cache.GetWithDefaultValue<IEnumerable<Culture>>(
        "cultures",
        () => this.storage.GetRepository<ICultureRepository>().All().ToList(),
        new CacheEntryOptions(priority: CacheEntryPriority.NeverRemove)
      );
    }
  }
}