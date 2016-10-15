// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Infrastructure;

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class BackendStyleSheetViewModelFactory : ViewModelFactoryBase
  {
    public BackendStyleSheetViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public BackendStyleSheetViewModel Create(BackendStyleSheet backendStyleSheet)
    {
      return new BackendStyleSheetViewModel()
      {
        Url = backendStyleSheet.Url,
        Position = backendStyleSheet.Position
      };
    }
  }
}