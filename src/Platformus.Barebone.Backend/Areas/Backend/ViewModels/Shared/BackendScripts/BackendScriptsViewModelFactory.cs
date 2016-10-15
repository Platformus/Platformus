// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Infrastructure;

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class BackendScriptsViewModelFactory : ViewModelFactoryBase
  {
    public BackendScriptsViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public BackendScriptsViewModel Create()
    {
      List<BackendScriptViewModel> backendScriptViewModels = new List<BackendScriptViewModel>();

      foreach (IExtension extension in ExtensionManager.Extensions)
        if (extension is Platformus.Infrastructure.IExtension)
          if ((extension as Platformus.Infrastructure.IExtension).BackendMetadata != null && (extension as Platformus.Infrastructure.IExtension).BackendMetadata.BackendScripts != null)
            foreach (Platformus.Infrastructure.BackendScript backendScript in (extension as Platformus.Infrastructure.IExtension).BackendMetadata.BackendScripts)
              backendScriptViewModels.Add(new BackendScriptViewModelFactory(this.handler).Create(backendScript));

      return new BackendScriptsViewModel()
      {
        BackendScripts = backendScriptViewModels.OrderBy(bs => bs.Position)
      };
    }
  }
}