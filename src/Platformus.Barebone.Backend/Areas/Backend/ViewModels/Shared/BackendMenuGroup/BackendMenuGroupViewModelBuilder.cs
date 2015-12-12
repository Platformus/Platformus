// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Infrastructure;

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class BackendMenuGroupViewModelBuilder : ViewModelBuilderBase
  {
    public BackendMenuGroupViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public BackendMenuGroupViewModel Build(BackendMenuGroup backendMenuGroup)
    {
      return new BackendMenuGroupViewModel()
      {
        Name = backendMenuGroup.Name,
        Position = backendMenuGroup.Position
      };
    }
  }
}