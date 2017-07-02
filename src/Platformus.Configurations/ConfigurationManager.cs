// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Configurations.Data.Abstractions;
using Platformus.Configurations.Data.Entities;

namespace Platformus.Configurations
{
  public class ConfigurationManager
  {
    private static IDictionary<string, string> values;

    private IStorage storage;

    public string this[string configurationCode, string variableCode]
    {
      get
      {
        if (ConfigurationManager.values == null)
          ConfigurationManager.values = new Dictionary<string, string>();

        string key = string.Format("{0}:{1}", configurationCode, variableCode);

        if (ConfigurationManager.values.Keys.Contains(key))
          return ConfigurationManager.values[key];

        Configuration configuration = this.storage.GetRepository<IConfigurationRepository>().WithCode(configurationCode);

        if (configuration == null)
          return null;

        Variable variable = this.storage.GetRepository<IVariableRepository>().WithConfigurationIdAndCode(configuration.Id, variableCode);

        if (variable == null)
          return null;

        ConfigurationManager.values[key] = variable.Value;
        return variable.Value;
      }
    }

    public ConfigurationManager(IStorage storage)
    {
      this.storage = storage;
    }

    public static void InvalidateCache()
    {
      ConfigurationManager.values = null;
    }
  }
}