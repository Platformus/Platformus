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
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public IndexViewModel Create(string orderBy, string direction, int skip, int take)
    {
      IFileRepository fileRepository = this.handler.Storage.GetRepository<IFileRepository>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory(this.handler).Create(
          orderBy, direction, skip, take, fileRepository.Count(),
          new[] {
            new GridColumnViewModelFactory(this.handler).Create("Name", "Name"),
            new GridColumnViewModelFactory(this.handler).Create("Size", "Size"),
            new GridColumnViewModelFactory(this.handler).BuildEmpty()
          },
          fileRepository.Range(orderBy, direction, skip, take).Select(f => new FileViewModelFactory(this.handler).Create(f)),
          "_File"
        )
      };
    }
  }
}