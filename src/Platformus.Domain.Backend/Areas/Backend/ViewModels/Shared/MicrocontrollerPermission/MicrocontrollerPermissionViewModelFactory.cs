// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Security.Backend.ViewModels.Shared;
using Platformus.Security.Data.Entities;

namespace Platformus.Domain.Backend.ViewModels.Shared
{
  public class MicrocontrollerPermissionViewModelFactory : ViewModelFactoryBase
  {
    public MicrocontrollerPermissionViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public MicrocontrollerPermissionViewModel Create(Microcontroller microcontroller, Permission permission)
    {
      MicrocontrollerPermission microcontrollerPermission = null;

      if (microcontroller != null)
        microcontrollerPermission = this.RequestHandler.Storage.GetRepository<IMicrocontrollerPermissionRepository>().WithKey(microcontroller.Id, permission.Id);

      return new MicrocontrollerPermissionViewModel()
      {
        Permission = new PermissionViewModelFactory(this.RequestHandler).Create(permission),
        IsAssigned = microcontrollerPermission != null
      };
    }
  }
}