// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.Extensions.Localization;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Designers.Backend.ViewModels.Shared;

namespace Platformus.Designers.Backend.ViewModels.Styles
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(string orderBy, string direction, int skip, int take, string filter)
    {
      string stylesPath = PathManager.GetStylesPath(this.RequestHandler);
      IStringLocalizer<IndexViewModelFactory> localizer = this.RequestHandler.GetService<IStringLocalizer<IndexViewModelFactory>>();

      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, FileSystemRepository.CountFiles(stylesPath, "*.css", filter),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create(localizer["Filename"], "Filename"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          FileSystemRepository.GetFiles(stylesPath, "*.css", filter, orderBy, direction, skip, take).Select(fi => new StyleViewModelFactory(this.RequestHandler).Create(fi)),
          "_Style"
        )
      };
    }
  }
}