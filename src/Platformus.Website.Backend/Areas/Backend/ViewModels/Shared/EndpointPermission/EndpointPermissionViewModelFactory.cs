// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels;
using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Data.Entities;
using Platformus.Website.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public class EndpointPermissionViewModelFactory : ViewModelFactoryBase
  {
    public EndpointPermissionViewModel Create(Permission permission, EndpointPermission endpointPermission)
    {
      return new EndpointPermissionViewModel()
      {
        Permission = new PermissionViewModelFactory().Create(permission),
        IsAssigned = endpointPermission != null
      };
    }
  }
}