// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Configurations.Backend.ViewModels.Shared;
using Platformus.Configurations.Data.Abstractions;

namespace Platformus.Configurations.Backend.ViewModels.Configurations
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create()
    {
      return new IndexViewModel()
      {
        Configurations = this.RequestHandler.Storage.GetRepository<IConfigurationRepository>().All().ToList().Select(
          c => new ConfigurationViewModelFactory(this.RequestHandler).Create(c)
        )
      };
    }
  }
}