// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;
using Platformus.Core.Services.Abstractions;

namespace Platformus.Core.Services.Defaults;

public class DefaultCultureManager : ICultureManager
{
  private ICache cache;
  private IStorage storage;

  public DefaultCultureManager(ICache cache, IStorage storage)
  {
    this.cache = cache;
    this.storage = storage;
  }

  public async Task<Culture> GetCultureAsync(string id)
  {
    return (await this.GetCachedCulturesAsync()).FirstOrDefault(c => c.Id == id);
  }

  public async Task<Culture> GetNeutralCultureAsync()
  {
    return (await this.GetCachedCulturesAsync()).FirstOrDefault(c => c.IsNeutral);
  }

  public async Task<Culture> GetFrontendDefaultCultureAsync()
  {
    return (await this.GetCachedCulturesAsync()).FirstOrDefault(c => c.IsFrontendDefault);
  }

  public async Task<Culture> GetBackendDefaultCultureAsync()
  {
    return (await this.GetCachedCulturesAsync()).FirstOrDefault(c => c.IsBackendDefault);
  }

  public async Task<Culture> GetCurrentCultureAsync()
  {
    Culture currentCulture = (await this.GetCachedCulturesAsync()).FirstOrDefault(
      c => c.Id == CultureInfo.CurrentCulture.TwoLetterISOLanguageName
    );

    if (currentCulture == null)
      currentCulture = this.GetCachedCulturesAsync().Result.FirstOrDefault(
        c => c.Id == DefaultCulture.Id
      );

    return currentCulture;
  }

  public async Task<IEnumerable<Culture>> GetCulturesAsync()
  {
    return await this.GetCachedCulturesAsync();
  }

  public async Task<IEnumerable<Culture>> GetNotNeutralCulturesAsync()
  {
    return (await this.GetCachedCulturesAsync()).Where(c => !c.IsNeutral);
  }

  public void InvalidateCache()
  {
    this.cache.Remove("cultures");
  }

  private async Task<IEnumerable<Culture>> GetCachedCulturesAsync()
  {
    return await this.cache.GetWithDefaultValueAsync<IEnumerable<Culture>>(
      "cultures",
      async () => await this.storage.GetRepository<int, Culture, CultureFilter>().GetAllAsync(),
      new CacheEntryOptions(priority: CacheEntryPriority.NeverRemove)
    );
  }
}