// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;

namespace Platformus.ExtensionManager.Backend.ViewModels.Shared
{
  public class ExtensionViewModelFactory : ViewModelFactoryBase
  {
    public ExtensionViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public ExtensionViewModel Create(FileInfo extensionFileInfo)
    {
      Extension extension = new Platformus.ExtensionManager.ExtensionManager(this.RequestHandler).ReadExtension(extensionFileInfo.FullName);

      return new ExtensionViewModel()
      {
        Id = extension.Id,
        Name = extension.Name,
        Version = extension.Version
      };
    }
  }
}