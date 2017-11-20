// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;

namespace Platformus.ExtensionManager.Backend.ViewModels.ExtensionManager
{
  public class DeleteViewModelFactory : ViewModelFactoryBase
  {
    public DeleteViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public DeleteViewModel Create(string id)
    {
      Extension extension = new Platformus.ExtensionManager.ExtensionManager(this.RequestHandler).ReadExtension(
        PathManager.GetExtensionPath(this.RequestHandler, id + Path.DirectorySeparatorChar + "extension.json")
      );

      return new DeleteViewModel()
      {
        Id = extension.Id,
        Files = "extension.json\r\n" + string.Join("\r\n", extension.Files)
      };
    }
  }
}