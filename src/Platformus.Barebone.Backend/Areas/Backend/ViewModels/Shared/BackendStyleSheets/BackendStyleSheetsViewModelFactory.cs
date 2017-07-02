// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Infrastructure;
using Platformus.Infrastructure;

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class BackendStyleSheetsViewModelFactory : ViewModelFactoryBase
  {
    public BackendStyleSheetsViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public BackendStyleSheetsViewModel Create()
    {
      List<BackendStyleSheetViewModel> backendStyleSheetViewModels = new List<BackendStyleSheetViewModel>();

      foreach (IBackendMetadata backendMetadata in ExtensionManager.GetInstances<IBackendMetadata>())
        if (backendMetadata.BackendStyleSheets != null)
          foreach (BackendStyleSheet backendStyleSheet in backendMetadata.BackendStyleSheets)
            backendStyleSheetViewModels.Add(new BackendStyleSheetViewModelFactory(this.RequestHandler).Create(backendStyleSheet));

      return new BackendStyleSheetsViewModel()
      {
        BackendStyleSheets = backendStyleSheetViewModels.OrderBy(bss => bss.Position)
      };
    }
  }
}