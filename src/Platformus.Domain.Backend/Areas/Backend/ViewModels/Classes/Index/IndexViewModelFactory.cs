// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
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

    public IndexViewModel Create(string orderBy, string direction, int skip, int take)
    {
      IClassRepository @classRepository = this.RequestHandler.Storage.GetRepository<IClassRepository>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, @classRepository.Count(),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create("Parent"),
            new GridColumnViewModelFactory(this.RequestHandler).Create("Name", "Name"),
            new GridColumnViewModelFactory(this.RequestHandler).Create("Is abstract", "IsAbstract"),
            new GridColumnViewModelFactory(this.RequestHandler).Create("Is standalone", "IsStandalone"),
            new GridColumnViewModelFactory(this.RequestHandler).Create("Tabs"),
            new GridColumnViewModelFactory(this.RequestHandler).Create("Members"),
            new GridColumnViewModelFactory(this.RequestHandler).Create("Data sources"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          @classRepository.Range(orderBy, direction, skip, take).Select(c => new ClassViewModelFactory(this.RequestHandler).Create(c)),
          "_Class"
        )
      };
    }
  }
}