// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Routing.Backend.ViewModels.Shared;
using Platformus.Routing.Data.Abstractions;

namespace Platformus.Routing.Backend.ViewModels.DataSources
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(int endpointId, string orderBy, string direction, int skip, int take, string filter)
    {
      IDataSourceRepository dataSourceRepository = this.RequestHandler.Storage.GetRepository<IDataSourceRepository>();
      IStringLocalizer<IndexViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<IndexViewModelFactory>>();

      return new IndexViewModel()
      {
        EndpointId = endpointId,
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, dataSourceRepository.CountByEndpointId(endpointId, filter),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Code"], "Code"),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["C# class name"], "CSharpClassName"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          dataSourceRepository.FilteredByEndpointIdRange(endpointId, orderBy, direction, skip, take, filter).ToList().Select(ds => new DataSourceViewModelFactory(this.RequestHandler).Create(ds)),
          "_DataSource"
        )
      };
    }
  }
}