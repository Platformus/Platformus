// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Configuration.Data.Abstractions;
using Platformus.Configuration.Data.Models;

namespace Platformus.Configuration.Backend.ViewModels.Variables
{
  public class CreateOrEditViewModelBuilder : ViewModelBuilderBase
  {
    public CreateOrEditViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public CreateOrEditViewModel Build(int? id)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
        };

      Variable variable = this.handler.Storage.GetRepository<IVariableRepository>().WithKey((int)id);

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