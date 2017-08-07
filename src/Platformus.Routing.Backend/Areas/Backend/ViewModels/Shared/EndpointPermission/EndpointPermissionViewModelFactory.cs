// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Routing.Data.Abstractions;
using Platformus.Routing.Data.Entities;
using Platformus.Security.Backend.ViewModels.Shared;
using Platformus.Security.Data.Entities;

namespace Platformus.Routing.Backend.ViewModels.Shared
{
  public class EndpointPermissionViewModelFactory : ViewModelFactoryBase
  {
    public EndpointPermissionViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public EndpointPermissionViewModel Create(Endpoint endpoint, Permission permission)
    {
      EndpointPermission endpointPermission = null;

      if (endpoint != null)
        endpointPermission = this.RequestHandler.Storage.GetRepository<IEndpointPermissionRepository>().WithKey(endpoint.Id, permission.Id);

      return new EndpointPermissionViewModel()
      {
        Permission = new PermissionViewModelFactory(this.RequestHandler).Create(permission),
        IsAssigned = endpointPermission != null
      };
    }
  }
}