// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Content.Data.Models;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.Shared
{
  public class DataSourceViewModelFactory : ViewModelFactoryBase
  {
    public DataSourceViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public DataSourceViewModel Create(DataSource dataSource)
    {
      return new DataSourceViewModel()
      {
        Id = dataSource.Id,
        CShartClassName = dataSource.CSharpClassName
      };
    }
  }
}