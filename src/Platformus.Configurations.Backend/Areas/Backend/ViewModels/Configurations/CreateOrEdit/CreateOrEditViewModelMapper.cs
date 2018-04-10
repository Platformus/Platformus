// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Configurations.Data.Abstractions;
using Platformus.Configurations.Data.Entities;

namespace Platformus.Configurations.Backend.ViewModels.Configurations
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public Configuration Map(CreateOrEditViewModel createOrEdit)
    {
      Configuration configuration = new Configuration();

      if (createOrEdit.Id != null)
        configuration = this.RequestHandler.Storage.GetRepository<IConfigurationRepository>().WithKey((int)createOrEdit.Id);

      configuration.Code = createOrEdit.Code;
      configuration.Name = createOrEdit.Name;
      return configuration;
    }
  }
}