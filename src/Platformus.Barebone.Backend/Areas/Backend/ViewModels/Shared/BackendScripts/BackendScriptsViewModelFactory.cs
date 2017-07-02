// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Infrastructure;
using Platformus.Infrastructure;

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class BackendScriptsViewModelFactory : ViewModelFactoryBase
  {
    public BackendScriptsViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public BackendScriptsViewModel Create()
    {
      List<BackendScriptViewModel> backendScriptViewModels = new List<BackendScriptViewModel>();

      foreach (IBackendMetadata backendMetadata in ExtensionManager.GetInstances<IBackendMetadata>())
        if (backendMetadata.BackendScripts != null)
          foreach (BackendScript backendScript in backendMetadata.BackendScripts)
            backendScriptViewModels.Add(new BackendScriptViewModelFactory(this.RequestHandler).Create(backendScript));

      return new BackendScriptsViewModel()
      {
        BackendScripts = backendScriptViewModels.OrderBy(bs => bs.Position)
      };
    }
  }
}