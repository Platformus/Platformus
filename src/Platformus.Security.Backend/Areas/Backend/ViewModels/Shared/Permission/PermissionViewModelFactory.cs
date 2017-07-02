// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Backend.ViewModels.Shared
{
  public class PermissionViewModelFactory : ViewModelFactoryBase
  {
    public PermissionViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public PermissionViewModel Create(Permission permission)
    {
      return new PermissionViewModel()
      {
        Id = permission.Id,
        Name = permission.Name,
        Position = permission.Position
      };
    }
  }
}