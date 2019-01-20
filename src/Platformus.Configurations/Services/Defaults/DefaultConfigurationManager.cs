// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Abstractions;
using Platformus.Configurations.Data.Abstractions;
using Platformus.Configurations.Data.Entities;
using Platformus.Configurations.Services.Abstractions;

namespace Platformus.Configurations.Services.Defaults
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

        return this.cache.GetWithDefaultValue(
          "configuration:" + key,
          () =>
          {
            Configuration configuration = this.storage.GetRepository<IConfigurationRepository>().WithCode(configurationCode);

            if (configuration == null)
              return null;

            Variable variable = this.storage.GetRepository<IVariableRepository>().WithConfigurationIdAndCode(configuration.Id, variableCode);

            if (variable == null)
              return null;

            return variable.Value;
          },
          new CacheEntryOptions(priority: CacheEntryPriority.NeverRemove)
        );
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