// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Configurations.Data.Abstractions;
using Platformus.Configurations.Data.Entities;

namespace Platformus.Configurations.Backend.ViewModels.Variables
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

      Variable variable = this.RequestHandler.Storage.GetRepository<IVariableRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = variable.Id,
        Code = variable.Code,
        Name = variable.Name,
        Value = variable.Value,
        Position = variable.Position
      };
    }
  }
}