// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Content.Backend.ViewModels.Shared;
using Platformus.Content.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.DataTypes
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public IndexViewModel Create(string orderBy, string direction, int skip, int take)
    {
      IDataTypeRepository dataTypeRepository = this.handler.Storage.GetRepository<IDataTypeRepository>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory(this.handler).Create(
          orderBy, direction, skip, take, dataTypeRepository.Count(),
          new[] {
            new GridColumnViewModelFactory(this.handler).Create("Name", "Name"),
            new GridColumnViewModelFactory(this.handler).Create("Position", "Position"),
            new GridColumnViewModelFactory(this.handler).CreateEmpty()
          },
          dataTypeRepository.Range(orderBy, direction, skip, take).Select(dt => new DataTypeViewModelFactory(this.handler).Create(dt)),
          "_DataType"
        )
      };
    }
  }
}