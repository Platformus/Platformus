// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Globalization.Backend.ViewModels.Shared;
using Platformus.Globalization.Data.Abstractions;

namespace Platformus.Globalization.Backend.ViewModels.Cultures
{
  public class IndexViewModelBuilder : ViewModelBuilderBase
  {
    public IndexViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public IndexViewModel Build(string orderBy, string direction, int skip, int take)
    {
      ICultureRepository @classRepository = this.handler.Storage.GetRepository<ICultureRepository>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelBuilder(this.handler).Build(
          orderBy, direction, skip, take, @classRepository.Count(),
          new[] {
            new GridColumnViewModelBuilder(this.handler).Build("Name", "Name"),
            new GridColumnViewModelBuilder(this.handler).BuildEmpty()
          },
          @classRepository.Range(orderBy, direction, skip, take).Select(c => new CultureViewModelBuilder(this.handler).Build(c)),
          "_Culture"
        )
      };
    }
  }
}