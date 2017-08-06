// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Routing.Data.Entities;

namespace Platformus.Routing.Backend.ViewModels.Shared
{
  public class DataSourceViewModelFactory : ViewModelFactoryBase
  {
    public DataSourceViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public DataSourceViewModel Create(DataSource dataSource)
    {
      return new DataSourceViewModel()
      {
        Id = dataSource.Id,
        Code = dataSource.Code,
        CShartClassName = dataSource.CSharpClassName
      };
    }
  }
}