// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Content.Backend.ViewModels.Shared;
using Platformus.Content.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.Classes
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public IndexViewModel Create(string orderBy, string direction, int skip, int take)
    {
      IClassRepository @classRepository = this.handler.Storage.GetRepository<IClassRepository>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory(this.handler).Create(
          orderBy, direction, skip, take, @classRepository.Count(),
          new[] {
            new GridColumnViewModelFactory(this.handler).Create("Parent"),
            new GridColumnViewModelFactory(this.handler).Create("Name", "Name"),
            new GridColumnViewModelFactory(this.handler).Create("Is abstract", "IsAbstract"),
            new GridColumnViewModelFactory(this.handler).Create("Is standalone", "IsStandalone"),
            new GridColumnViewModelFactory(this.handler).Create("Tabs"),
            new GridColumnViewModelFactory(this.handler).Create("Members"),
            new GridColumnViewModelFactory(this.handler).Create("Data sources"),
            new GridColumnViewModelFactory(this.handler).BuildEmpty()
          },
          @classRepository.Range(orderBy, direction, skip, take).Select(c => new ClassViewModelFactory(this.handler).Create(c)),
          "_Class"
        )
      };
    }
  }
}