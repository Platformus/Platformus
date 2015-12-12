// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Infrastructure;

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class BackendMenuViewModelBuilder : ViewModelBuilderBase
  {
    public BackendMenuViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public BackendMenuViewModel Build()
    {
      List<BackendMenuGroupViewModel> backendMenuGroupViewModels = new List<BackendMenuGroupViewModel>();

      foreach (IExtension extension in ExtensionManager.Extensions)
      {
        if (extension is Platformus.Infrastructure.IExtension)
        {
          foreach (Platformus.Infrastructure.BackendMenuGroup backendMenuGroup in (extension as Platformus.Infrastructure.IExtension).BackendMenuGroups)
          {
            List<BackendMenuItemViewModel> backendMenuItemViewModels = new List<BackendMenuItemViewModel>();

            foreach (Platformus.Infrastructure.BackendMenuItem backendMenuItem in backendMenuGroup.BackendMenuItems)
              backendMenuItemViewModels.Add(new BackendMenuItemViewModelBuilder(this.handler).Build(backendMenuItem));

            BackendMenuGroupViewModel backendMenuGroupViewModel = this.GetBackendMenuGroup(backendMenuGroupViewModels, backendMenuGroup);

            if (backendMenuGroupViewModel.BackendMenuItems != null)
              backendMenuItemViewModels.AddRange(backendMenuGroupViewModel.BackendMenuItems);

            backendMenuGroupViewModel.BackendMenuItems = backendMenuItemViewModels.OrderBy(bmi => bmi.Position);
          }
        }
      }

      return new BackendMenuViewModel()
      {
        BackendMenuGroups = backendMenuGroupViewModels.OrderBy(bmg => bmg.Position)
      };
    }

    private BackendMenuGroupViewModel GetBackendMenuGroup(List<BackendMenuGroupViewModel> backendMenuGroupViewModels, Platformus.Infrastructure.BackendMenuGroup backendMenuGroup)
    {
      BackendMenuGroupViewModel backendMenuGroupViewModel = backendMenuGroupViewModels.FirstOrDefault(bmg => bmg.Name == backendMenuGroup.Name);

      if (backendMenuGroupViewModel == null)
      {
        backendMenuGroupViewModel = new BackendMenuGroupViewModelBuilder(this.handler).Build(backendMenuGroup);
        backendMenuGroupViewModels.Add(backendMenuGroupViewModel);
      }

      return backendMenuGroupViewModel;
    }
  }
}