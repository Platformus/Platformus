// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.FileManager.Backend.ViewModels.Shared;
using Platformus.FileManager.Data.Abstractions;

namespace Platformus.FileManager.Backend.ViewModels.FileManager
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(string orderBy, string direction, int skip, int take, string filter)
    {
      IFileRepository fileRepository = this.RequestHandler.Storage.GetRepository<IFileRepository>();
      IStringLocalizer<IndexViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<IndexViewModelFactory>>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, fileRepository.Count(filter),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Name"], "Name"),
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Size"], "Size"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          fileRepository.Range(orderBy, direction, skip, take, filter).ToList().Select(f => new FileViewModelFactory(this.RequestHandler).Create(f)),
          "_File"
        )
      };
    }
  }
}