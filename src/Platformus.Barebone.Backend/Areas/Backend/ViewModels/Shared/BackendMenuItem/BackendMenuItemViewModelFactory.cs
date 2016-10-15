// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Infrastructure;

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class BackendMenuItemViewModelFactory : ViewModelFactoryBase
  {
    public BackendMenuItemViewModelFactory(IHandler handler)
      : base(handler)
    {
    }

    public BackendMenuItemViewModel Create(BackendMenuItem backendMenuItem)
    {
      return new BackendMenuItemViewModel()
      {
        Url = backendMenuItem.Url,
        Name = backendMenuItem.Name,
        Position = backendMenuItem.Position
      };
    }
  }
}