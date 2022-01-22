// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public static class DataTypeViewModelFactory
  {
    public static DataTypeViewModel Create(DataType dataType)
    {
      return new DataTypeViewModel()
      {
        Id = dataType.Id,
        StorageDataType = dataType.StorageDataType,
        ParameterEditorCode = dataType.ParameterEditorCode,
        Name = dataType.Name,
        Position = dataType.Position
      };
    }
  }
}