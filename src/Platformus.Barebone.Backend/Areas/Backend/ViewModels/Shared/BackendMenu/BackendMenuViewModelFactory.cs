// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Infrastructure;
using Platformus.Infrastructure;

namespace Platformus.Barebone.Backend.ViewModels.Shared
{
  public class BackendMenuViewModelFactory : ViewModelFactoryBase
  {
    public BackendMenuViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public BackendMenuViewModel Create()
    {
      List<BackendMenuGroupViewModel> backendMenuGroupViewModels = new List<BackendMenuGroupViewModel>();

      foreach (IBackendMetadata backendMetadata in ExtensionManager.GetInstances<IBackendMetadata>())
      {
        IEnumerable<BackendMenuGroup> backendMenuItems = backendMetadata.GetBackendMenuGroups(
          this.RequestHandler.HttpContext.RequestServices
        );

        foreach (BackendMenuGroup backendMenuGroup in backendMenuItems)
        {
          List<BackendMenuItemViewModel> backendMenuItemViewModels = new List<BackendMenuItemViewModel>();

          foreach (BackendMenuItem backendMenuItem in backendMenuGroup.BackendMenuItems)
            if (this.RequestHandler.HttpContext.User.Claims.Any(c => backendMenuItem.PermissionCodes.Any(pc => string.Equals(c.Value, pc, StringComparison.OrdinalIgnoreCase)) || string.Equals(c.Value, "DoEverything", StringComparison.OrdinalIgnoreCase)))
              backendMenuItemViewModels.Add(new BackendMenuItemViewModelFactory(this.RequestHandler).Create(backendMenuItem));

          BackendMenuGroupViewModel backendMenuGroupViewModel = this.GetBackendMenuGroup(backendMenuGroupViewModels, backendMenuGroup);

          if (backendMenuGroupViewModel.BackendMenuItems != null)
            backendMenuItemViewModels.AddRange(backendMenuGroupViewModel.BackendMenuItems);

          backendMenuGroupViewModel.BackendMenuItems = backendMenuItemViewModels.OrderBy(bmi => bmi.Position);
        }
      }

      return new BackendMenuViewModel()
      {
        BackendMenuGroups = backendMenuGroupViewModels.Where(bmg => bmg.BackendMenuItems.Count() != 0).OrderBy(bmg => bmg.Position)
      };
    }

    private BackendMenuGroupViewModel GetBackendMenuGroup(List<BackendMenuGroupViewModel> backendMenuGroupViewModels, Platformus.Infrastructure.BackendMenuGroup backendMenuGroup)
    {
      BackendMenuGroupViewModel backendMenuGroupViewModel = backendMenuGroupViewModels.FirstOrDefault(bmg => bmg.Name == backendMenuGroup.Name);

      if (backendMenuGroupViewModel == null)
      {
        backendMenuGroupViewModel = new BackendMenuGroupViewModelFactory(this.RequestHandler).Create(backendMenuGroup);
        backendMenuGroupViewModels.Add(backendMenuGroupViewModel);
      }

      return backendMenuGroupViewModel;
    }
  }
}