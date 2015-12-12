// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Static.Backend.ViewModels.Shared;
using Platformus.Static.Data.Abstractions;

namespace Platformus.Static.Backend.ViewModels.Files
{
  public class IndexViewModelBuilder : ViewModelBuilderBase
  {
    public IndexViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public IndexViewModel Build(string orderBy, string direction, int skip, int take)
    {
      IFileRepository fileRepository = this.handler.Storage.GetRepository<IFileRepository>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelBuilder(this.handler).Build(
          orderBy, direction, skip, take, fileRepository.Count(),
          new[] {
            new GridColumnViewModelBuilder(this.handler).Build("Name", "Name"),
            new GridColumnViewModelBuilder(this.handler).Build("Size", "Size"),
            new GridColumnViewModelBuilder(this.handler).BuildEmpty()
          },
          fileRepository.Range(orderBy, direction, skip, take).Select(f => new FileViewModelBuilder(this.handler).Build(f)),
          "_File"
        )
      };
    }
  }
}