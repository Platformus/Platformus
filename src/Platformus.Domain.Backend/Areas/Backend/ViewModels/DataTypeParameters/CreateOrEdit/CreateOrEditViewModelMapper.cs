// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.DataTypeParameters
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public DataTypeParameter Map(CreateOrEditViewModel createOrEdit)
    {
      DataTypeParameter dataTypeParameter = new DataTypeParameter();

      if (createOrEdit.Id != null)
        dataTypeParameter = this.RequestHandler.Storage.GetRepository<IDataTypeParameterRepository>().WithKey((int)createOrEdit.Id);

      else dataTypeParameter.DataTypeId = createOrEdit.DataTypeId;

      dataTypeParameter.JavaScriptEditorClassName = createOrEdit.JavaScriptEditorClassName;
      dataTypeParameter.Code = createOrEdit.Code;
      dataTypeParameter.Name = createOrEdit.Name;
      return dataTypeParameter;
    }
  }
}