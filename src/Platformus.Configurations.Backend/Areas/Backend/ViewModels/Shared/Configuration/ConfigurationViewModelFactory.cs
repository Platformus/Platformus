// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Configurations.Data.Abstractions;
using Platformus.Configurations.Data.Entities;

namespace Platformus.Configurations.Backend.ViewModels.Shared
{
  public class ConfigurationViewModelFactory : ViewModelFactoryBase
  {
    public ConfigurationViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public ConfigurationViewModel Create(Configuration configuration)
    {
      return new ConfigurationViewModel()
      {
        Id = configuration.Id,
        Name = configuration.Name,
        Variables = this.RequestHandler.Storage.GetRepository<IVariableRepository>().FilteredByConfigurationId(configuration.Id).Select(
          v => new VariableViewModelFactory(this.RequestHandler).Create(v)
        )
      };
    }
  }
}