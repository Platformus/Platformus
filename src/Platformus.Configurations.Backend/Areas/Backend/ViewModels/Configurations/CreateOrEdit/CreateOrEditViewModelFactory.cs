// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Configurations.Data.Abstractions;
using Platformus.Configurations.Data.Entities;

namespace Platformus.Configurations.Backend.ViewModels.Configurations
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CreateOrEditViewModel Create(int? id)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
        };

      Configuration configuration = this.RequestHandler.Storage.GetRepository<IConfigurationRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = configuration.Id,
        Code = configuration.Code,
        Name = configuration.Name
      };
    }
  }
}