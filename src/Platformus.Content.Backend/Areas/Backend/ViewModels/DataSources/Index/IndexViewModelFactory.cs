// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Content.Backend.ViewModels.Shared;
using Platformus.Content.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.DataSources
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public IndexViewModel Create(int classId, string orderBy, string direction, int skip, int take)
    {
      IDataSourceRepository dataSourceRepository = this.handler.Storage.GetRepository<IDataSourceRepository>();

      return new IndexViewModel()
      {
        ClassId = classId,
        Grid = new GridViewModelFactory(this.handler).Create(
          orderBy, direction, skip, take, dataSourceRepository.CountByClassId(classId),
          new[] {
            new GridColumnViewModelFactory(this.handler).Create("C# class name"),
            new GridColumnViewModelFactory(this.handler).CreateEmpty()
          },
          dataSourceRepository.FilteredByClassIdRange(classId, orderBy, direction, skip, take).Select(ds => new DataSourceViewModelFactory(this.handler).Create(ds)),
          "_DataSource"
        )
      };
    }
  }
}