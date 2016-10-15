// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Content.Backend.ViewModels.Shared;
using Platformus.Content.Data.Abstractions;
using Platformus.Globalization.Backend.ViewModels;

namespace Platformus.Content.Backend.ViewModels.Tabs
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public IndexViewModel Create(int classId, string orderBy, string direction, int skip, int take)
    {
      ITabRepository tabRepository = this.handler.Storage.GetRepository<ITabRepository>();

      return new IndexViewModel()
      {
        ClassId = classId,
        Grid = new GridViewModelFactory(this.handler).Create(
          orderBy, direction, skip, take, tabRepository.CountByClassId(classId),
          new[] {
            new GridColumnViewModelFactory(this.handler).Create("Name", "Name"),
            new GridColumnViewModelFactory(this.handler).Create("Position", "Position"),
            new GridColumnViewModelFactory(this.handler).BuildEmpty()
          },
          tabRepository.FilteredByClassRange(classId, orderBy, direction, skip, take).Select(t => new TabViewModelFactory(this.handler).Create(t)),
          "_Tab"
        )
      };
    }
  }
}