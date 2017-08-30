// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Domain.Backend.ViewModels.Shared;
using Platformus.Domain.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Classes
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(string orderBy, string direction, int skip, int take, string filter)
    {
      IClassRepository @classRepository = this.RequestHandler.Storage.GetRepository<IClassRepository>();
      IStringLocalizer<IndexViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<IndexViewModelFactory>>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, @classRepository.Count(filter),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Parent class"]),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Name"], "Name"),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Is abstract"], "IsAbstract"),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Tabs"]),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Members"]),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          @classRepository.Range(orderBy, direction, skip, take, filter).ToList().Select(c => new ClassViewModelFactory(this.RequestHandler).Create(c)),
          "_Class"
        )
      };
    }
  }
}