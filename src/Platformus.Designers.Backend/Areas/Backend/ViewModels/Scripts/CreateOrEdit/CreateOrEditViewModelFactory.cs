// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;

namespace Platformus.Designers.Backend.ViewModels.Scripts
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CreateOrEditViewModel Create(string filename)
    {
      FileInfo scriptFileInfo = string.IsNullOrEmpty(filename) ? null : new FileInfo(PathManager.GetScriptPath(this.RequestHandler, filename));

      return new CreateOrEditViewModel()
      {
        Id = filename,
        Filename = scriptFileInfo == null ? null : scriptFileInfo.Name,
        Content = scriptFileInfo == null ? null : File.ReadAllText(scriptFileInfo.FullName)
      };
    }
  }
}