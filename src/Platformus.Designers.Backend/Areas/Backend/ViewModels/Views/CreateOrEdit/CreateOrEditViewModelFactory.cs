// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;

namespace Platformus.Designers.Backend.ViewModels.Views
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CreateOrEditViewModel Create(string subdirectory, string filename)
    {
      FileInfo viewFileInfo = string.IsNullOrEmpty(filename) ? null : new FileInfo(PathManager.GetViewPath(this.RequestHandler, subdirectory, filename));

      return new CreateOrEditViewModel()
      {
        Filename = viewFileInfo == null ? null : viewFileInfo.Name,
        Content = viewFileInfo == null ? null : File.ReadAllText(viewFileInfo.FullName)
      };
    }
  }
}