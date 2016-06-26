// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Infrastructure;

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class BackendStyleSheetsViewModelBuilder : ViewModelBuilderBase
  {
    public BackendStyleSheetsViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public BackendStyleSheetsViewModel Build()
    {
      List<BackendStyleSheetViewModel> backendStyleSheetViewModels = new List<BackendStyleSheetViewModel>();

      foreach (IExtension extension in ExtensionManager.Extensions)
        if (extension is Platformus.Infrastructure.IExtension)
          if ((extension as Platformus.Infrastructure.IExtension).BackendMetadata != null && (extension as Platformus.Infrastructure.IExtension).BackendMetadata.BackendStyleSheets != null)
            foreach (Platformus.Infrastructure.BackendStyleSheet backendStyleSheet in (extension as Platformus.Infrastructure.IExtension).BackendMetadata.BackendStyleSheets)
              backendStyleSheetViewModels.Add(new BackendStyleSheetViewModelBuilder(this.handler).Build(backendStyleSheet));

      return new BackendStyleSheetsViewModel()
      {
        BackendStyleSheets = backendStyleSheetViewModels.OrderBy(bss => bss.Position)
      };
    }
  }
}