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
  public class IndexViewModelBuilder : ViewModelBuilderBase
  {
    public IndexViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public IndexViewModel Build(string orderBy, string direction, int skip, int take)
    {
      IClassRepository @classRepository = this.handler.Storage.GetRepository<IClassRepository>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelBuilder(this.handler).Build(
          orderBy, direction, skip, take, @classRepository.Count(),
          new[] {
            new GridColumnViewModelBuilder(this.handler).Build("Name", "Name"),
            new GridColumnViewModelBuilder(this.handler).Build("Tabs"),
            new GridColumnViewModelBuilder(this.handler).Build("Properties"),
            new GridColumnViewModelBuilder(this.handler).Build("Data sources"),
            new GridColumnViewModelBuilder(this.handler).BuildEmpty()
          },
          @classRepository.Range(orderBy, direction, skip, take).Select(c => new ClassViewModelBuilder(this.handler).Build(c)),
          "_Class"
        )
      };
    }
  }
}