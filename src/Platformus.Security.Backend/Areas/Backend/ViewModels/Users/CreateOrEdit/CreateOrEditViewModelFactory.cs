// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Backend.ViewModels.Shared;
using Platformus.Security.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Backend.ViewModels.Users
{
  public class CreateOrEditViewModelFactory : ViewModelFactoryBase
  {
    public CreateOrEditViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public CreateOrEditViewModel Create(int? id)
    {
      if (id == null)
        return new CreateOrEditViewModel()
        {
          UserRoles = this.GetUserRoles()
        };

      User user = this.RequestHandler.Storage.GetRepository<IUserRepository>().WithKey((int)id);

      return new CreateOrEditViewModel()
      {
        Id = user.Id,
        Name = user.Name,
        UserRoles = this.GetUserRoles(user)
      };
    }

    public IEnumerable<UserRoleViewModel> GetUserRoles(User user = null)
    {
      return this.RequestHandler.Storage.GetRepository<IRoleRepository>().All().Select(
        r => new UserRoleViewModelFactory(this.RequestHandler).Create(user, r)
      );
    }
  }
}