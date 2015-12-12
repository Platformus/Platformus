// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Backend.ViewModels.Shared
{
  public class UserRoleViewModelBuilder : ViewModelBuilderBase
  {
    public UserRoleViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public UserRoleViewModel Build(User user, Role role)
    {
      UserRole userRole = null;

      if (user != null)
        userRole = this.handler.Storage.GetRepository<IUserRoleRepository>().WithKey(user.Id, role.Id);

      return new UserRoleViewModel()
      {
        Role = new RoleViewModelBuilder(this.handler).Build(role),
        IsAssigned = userRole != null
      };
    }
  }
}