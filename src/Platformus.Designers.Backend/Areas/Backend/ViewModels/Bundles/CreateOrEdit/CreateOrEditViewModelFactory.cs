// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;

namespace Platformus.Designers.Backend.ViewModels.Bundles
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CreateOrEditViewModel Create(string filename)
    {
      FileInfo bundleFileInfo = string.IsNullOrEmpty(filename) ? null : new FileInfo(PathManager.GetBundlePath(this.RequestHandler, filename));

      return new CreateOrEditViewModel()
      {
        Id = filename,
        Filename = bundleFileInfo == null ? null : bundleFileInfo.Name,
        Content = bundleFileInfo == null ? null : File.ReadAllText(bundleFileInfo.FullName)
      };
    }
  }
}