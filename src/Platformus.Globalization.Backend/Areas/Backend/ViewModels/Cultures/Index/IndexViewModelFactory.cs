// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Globalization.Backend.ViewModels.Shared;
using Platformus.Globalization.Data.Abstractions;

namespace Platformus.Globalization.Backend.ViewModels.Cultures
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public IndexViewModel Create(string orderBy, string direction, int skip, int take)
    {
      ICultureRepository @classRepository = this.handler.Storage.GetRepository<ICultureRepository>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory(this.handler).Create(
          orderBy, direction, skip, take, @classRepository.Count(),
          new[] {
            new GridColumnViewModelFactory(this.handler).Create("Name", "Name"),
            new GridColumnViewModelFactory(this.handler).CreateEmpty()
          },
          @classRepository.Range(orderBy, direction, skip, take).Select(c => new CultureViewModelFactory(this.handler).Create(c)),
          "_Culture"
        )
      };
    }
  }
}