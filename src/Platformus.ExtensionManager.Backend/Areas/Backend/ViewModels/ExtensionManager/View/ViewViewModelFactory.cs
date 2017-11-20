// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;

namespace Platformus.ExtensionManager.Backend.ViewModels.ExtensionManager
{
  public class ViewViewModelFactory : ViewModelFactoryBase
  {
    public ViewViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public ViewViewModel Create(string id)
    {
      Extension extension = new Platformus.ExtensionManager.ExtensionManager(this.RequestHandler).ReadExtension(
        PathManager.GetExtensionPath(this.RequestHandler, id + Path.DirectorySeparatorChar + "extension.json")
      );

      return new ViewViewModel()
      {
        Id = extension.Id,
        Name = extension.Name,
        Description = extension.Description,
        Url = extension.Url,
        Authors = extension.Authors,
        Version = extension.Version
      };
    }
  }
}