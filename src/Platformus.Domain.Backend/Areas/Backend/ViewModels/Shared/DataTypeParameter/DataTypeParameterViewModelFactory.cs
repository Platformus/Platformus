// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Domain.Data.Entities;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Shared
{
  public class DataTypeParameterViewModelFactory : ViewModelFactoryBase
  {
    public DataTypeParameterViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public DataTypeParameterViewModel Create(DataTypeParameter dataTypeParameter)
    {
      return new DataTypeParameterViewModel()
      {
        Id = dataTypeParameter.Id,
        Name = dataTypeParameter.Name
      };
    }
  }
}