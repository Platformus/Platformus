// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Shared
{
  public class DataTypeViewModelFactory : ViewModelFactoryBase
  {
    public DataTypeViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public DataTypeViewModel Create(DataType dataType)
    {
      return new DataTypeViewModel()
      {
        Id = dataType.Id,
        StorageDataType = dataType.StorageDataType,
        Name = dataType.Name,
        Position = dataType.Position
      };
    }
  }
}