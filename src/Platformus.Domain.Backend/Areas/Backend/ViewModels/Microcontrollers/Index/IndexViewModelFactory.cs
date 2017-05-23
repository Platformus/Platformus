// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Domain.Backend.ViewModels.Shared;
using Platformus.Domain.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Domain.Backend.ViewModels.Microcontrollers
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(string orderBy, string direction, int skip, int take, string filter)
    {
      IMicrocontrollerRepository microcontrollerRepository = this.RequestHandler.Storage.GetRepository<IMicrocontrollerRepository>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, microcontrollerRepository.Count(filter),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create("Name", "Name"),
            new GridColumnViewModelFactory(this.RequestHandler).Create("URL template", "UrlTemplate"),
            new GridColumnViewModelFactory(this.RequestHandler).Create("View name", "ViewName"),
            new GridColumnViewModelFactory(this.RequestHandler).Create("Data sources"),
            new GridColumnViewModelFactory(this.RequestHandler).Create("Position", "Position"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          microcontrollerRepository.Range(orderBy, direction, skip, take, filter).ToList().Select(m => new MicrocontrollerViewModelFactory(this.RequestHandler).Create(m)),
          "_Microcontroller"
        )
      };
    }
  }
}