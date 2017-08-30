// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Domain.Backend.ViewModels.Shared;
using Platformus.Domain.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.DataTypeParameters
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(int dataTypeId, string orderBy, string direction, int skip, int take, string filter)
    {
      IDataTypeParameterRepository dataTypeParameterRepository = this.RequestHandler.Storage.GetRepository<IDataTypeParameterRepository>();
      IStringLocalizer<IndexViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<IndexViewModelFactory>>();

      return new IndexViewModel()
      {
        DataTypeId = dataTypeId,
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, dataTypeParameterRepository.CountByDataTypeId(dataTypeId, filter),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Name"], "Name"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          dataTypeParameterRepository.FilteredByDataTypeIdRange(dataTypeId, orderBy, direction, skip, take, filter).Select(dtp => new DataTypeParameterViewModelFactory(this.RequestHandler).Create(dtp)),
          "_DataTypeParameter"
        )
      };
    }
  }
}