// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.DataTypes
{
  public class CreateOrEditViewModelMapper : ViewModelMapperBase
  {
    public CreateOrEditViewModelMapper(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public DataType Map(CreateOrEditViewModel createOrEdit)
    {
      DataType dataType = new DataType();

      if (createOrEdit.Id != null)
        dataType = this.RequestHandler.Storage.GetRepository<IDataTypeRepository>().WithKey((int)createOrEdit.Id);

      dataType.StorageDataType = createOrEdit.StorageDataType;
      dataType.JavaScriptEditorClassName = createOrEdit.JavaScriptEditorClassName;
      dataType.Name = createOrEdit.Name;
      dataType.Position = createOrEdit.Position;
      return dataType;
    }
  }
}