// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Globalization.Backend.ViewModels.Shared;
using Platformus.Globalization.Data.Abstractions;

namespace Platformus.Globalization.Backend.ViewModels.Cultures
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(string orderBy, string direction, int skip, int take, string filter)
    {
      ICultureRepository cultureRepository = this.RequestHandler.Storage.GetRepository<ICultureRepository>();
      IStringLocalizer<IndexViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<IndexViewModelFactory>>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, cultureRepository.Count(filter),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Name"], "Name"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          cultureRepository.Range(orderBy, direction, skip, take, filter).ToList().Select(c => new CultureViewModelFactory(this.RequestHandler).Create(c)),
          "_Culture"
        )
      };
    }
  }
}