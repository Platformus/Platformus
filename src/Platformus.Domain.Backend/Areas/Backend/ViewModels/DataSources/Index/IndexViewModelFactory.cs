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

    public IndexViewModel Create(int classId, string orderBy, string direction, int skip, int take)
    {
      IDataSourceRepository dataSourceRepository = this.RequestHandler.Storage.GetRepository<IDataSourceRepository>();

      return new IndexViewModel()
      {
        ClassId = classId,
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, dataSourceRepository.CountByClassId(classId),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create("C# class name"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          dataSourceRepository.FilteredByClassIdRange(classId, orderBy, direction, skip, take).Select(ds => new DataSourceViewModelFactory(this.RequestHandler).Create(ds)),
          "_DataSource"
        )
      };
    }
  }
}