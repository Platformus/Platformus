// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Core.Primitives;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.DataTypes
{
  public static class CreateOrEditViewModelFactory
  {
    public static CreateOrEditViewModel Create(DataType dataType)
    {
      if (dataType == null)
        return new CreateOrEditViewModel()
        {
          StorageDataTypeOptions = GetStorageDataTypeOptions()
        };

      return new CreateOrEditViewModel()
      {
        Id = dataType.Id,
        StorageDataType = dataType.StorageDataType,
        StorageDataTypeOptions = GetStorageDataTypeOptions(),
        JavaScriptEditorClassName = dataType.JavaScriptEditorClassName,
        Name = dataType.Name,
        Position = dataType.Position
      };
    }

    private static IEnumerable<Option> GetStorageDataTypeOptions()
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