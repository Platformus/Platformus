// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Backend.ViewModels.Shared
{
  public class UserRoleViewModelFactory : ViewModelFactoryBase
  {
    public UserRoleViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public UserRoleViewModel Create(User user, Role role)
    {
      UserRole userRole = null;

      if (user != null)
        userRole = this.RequestHandler.Storage.GetRepository<IUserRoleRepository>().WithKey(user.Id, role.Id);

      return new UserRoleViewModel()
      {
        Role = new RoleViewModelFactory(this.RequestHandler).Create(role),
        IsAssigned = userRole != null
      };
    }
  }
}