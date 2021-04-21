// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Magicalizer.Data.Repositories.Abstractions;
using Platformus.Core.Data.Entities;
using Platformus.Core.Filters;
using Platformus.Core.Services.Abstractions;

namespace Platformus.Core.Services.Defaults
{
  public class DefaultConfigurationManager : IConfigurationManager
  {
    private ICache cache;
    private IStorage storage;

    public string this[string configurationCode, string variableCode]
    {
      get
      {
        string key = string.Format("{0}:{1}", configurationCode, variableCode);

        return this.cache.GetWithDefaultValueAsync(
          "configuration:" + key,
          async () =>
          {
            Variable variable = (await this.storage.GetRepository<int, Variable, VariableFilter>().GetAllAsync(
              new VariableFilter(configuration: new ConfigurationFilter(code: configurationCode), code: variableCode)
            )).FirstOrDefault();

            if (variable == null)
              return null;

            return variable.Value;
          },
          new CacheEntryOptions(priority: CacheEntryPriority.NeverRemove)
        ).Result;
      }
    }

    public DefaultConfigurationManager(ICache cache, IStorage storage)
    {
      this.cache = cache;
      this.storage = storage;
    }

    public void InvalidateCache()
    {
      this.cache.RemoveAll();
    }
  }
}