// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Infrastructure;

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class BackendMenuItemViewModelBuilder : ViewModelBuilderBase
  {
    public BackendMenuItemViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public BackendMenuItemViewModel Build(BackendMenuItem backendMenuItem)
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