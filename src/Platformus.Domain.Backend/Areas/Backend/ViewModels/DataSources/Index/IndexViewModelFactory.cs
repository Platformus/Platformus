// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Domain.Backend.ViewModels.Shared;
using Platformus.Domain.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.DataSources
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(int microcontrollerId, string orderBy, string direction, int skip, int take, string filter)
    {
      IDataSourceRepository dataSourceRepository = this.RequestHandler.Storage.GetRepository<IDataSourceRepository>();

      return new IndexViewModel()
      {
        MicrocontrollerId = microcontrollerId,
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, dataSourceRepository.CountByMicrocontrollerId(microcontrollerId, filter),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create("Code", "Code"),
            new GridColumnViewModelFactory(this.RequestHandler).Create("C# class name", "CSharpClassName"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          dataSourceRepository.FilteredByMicrocontrollerIdRange(microcontrollerId, orderBy, direction, skip, take, filter).ToList().Select(ds => new DataSourceViewModelFactory(this.RequestHandler).Create(ds)),
          "_DataSource"
        )
      };
    }
  }
}