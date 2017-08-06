// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Barebone;
using Platformus.Barebone.Primitives;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.DataTypes
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
          StorageDataTypeOptions = this.GetStorageDataTypeOptions()
        };

      DataType dataType = this.RequestHandler.Storage.GetRepository<IDataTypeRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = dataType.Id,
        StorageDataType = dataType.StorageDataType,
        StorageDataTypeOptions = this.GetStorageDataTypeOptions(),
        JavaScriptEditorClassName = dataType.JavaScriptEditorClassName,
        Name = dataType.Name,
        Position = dataType.Position
      };
    }

    private IEnumerable<Option> GetStorageDataTypeOptions()
    {
      return new Option[]
      {
        new Option(StorageDataType.Integer),
        new Option(StorageDataType.Decimal),
        new Option(StorageDataType.String),
        new Option(StorageDataType.DateTime)
      };
    }
  }
}