// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.DataTypeParameters
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CreateOrEditViewModel Create(int? id, int? dataTypeId)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
        };

      DataTypeParameter dataTypeParameter = this.RequestHandler.Storage.GetRepository<IDataTypeParameterRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = dataTypeParameter.Id,
        JavaScriptEditorClassName = dataTypeParameter.JavaScriptEditorClassName,
        Code = dataTypeParameter.Code,
        Name = dataTypeParameter.Name
      };
    }
  }
}