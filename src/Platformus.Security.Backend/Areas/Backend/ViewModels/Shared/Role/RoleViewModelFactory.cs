// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Backend.ViewModels.Shared
{
  public class RoleViewModelFactory : ViewModelFactoryBase
  {
    public RoleViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public RoleViewModel Create(Role role)
    {
      return new RoleViewModel()
      {
        Id = role.Id,
        Name = role.Name,
        Position = role.Position
      };
    }
  }
}