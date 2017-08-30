// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Domain.Backend.ViewModels.Shared;
using Platformus.Domain.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.DataTypes
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(string orderBy, string direction, int skip, int take, string filter)
    {
      IDataTypeRepository dataTypeRepository = this.RequestHandler.Storage.GetRepository<IDataTypeRepository>();
      IStringLocalizer<IndexViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<IndexViewModelFactory>>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, dataTypeRepository.Count(filter),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Storage data type"], "StorageDataType"),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Name"], "Name"),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Data type parameters"]),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Position"], "Position"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          dataTypeRepository.Range(orderBy, direction, skip, take, filter).ToList().Select(dt => new DataTypeViewModelFactory(this.RequestHandler).Create(dt)),
          "_DataType"
        )
      };
    }
  }
}