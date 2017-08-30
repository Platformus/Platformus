// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Designers.Backend.ViewModels.Shared;

namespace Platformus.Designers.Backend.ViewModels.Views
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(string subdirectory, string orderBy, string direction, int skip, int take, string filter)
    {
      string rootViewsPath = PathManager.GetViewsPath(this.RequestHandler, null);
      string viewsPath = PathManager.GetViewsPath(this.RequestHandler, subdirectory);
      IStringLocalizer<IndexViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<IndexViewModelFactory>>();

      return new IndexViewModel()
      {
        Subdirectory = string.IsNullOrEmpty(subdirectory) ? null : new DirectoryViewModelFactory(this.RequestHandler).Create(FileSystemRepository.GetDirectories(rootViewsPath).FirstOrDefault(di => di.Name.ToLower() == subdirectory)),
        Subdirectories = FileSystemRepository.GetDirectories(rootViewsPath).Select(di => new DirectoryViewModelFactory(this.RequestHandler).Create(di)),
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, FileSystemRepository.CountFiles(viewsPath, "*.cshtml", filter),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Filename"], "Filename"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          FileSystemRepository.GetFiles(viewsPath, "*.cshtml", filter, orderBy, direction, skip, take).Select(fi => new ViewViewModelFactory(this.RequestHandler).Create(subdirectory, fi)),
          "_View"
        )
      };
    }
  }
}