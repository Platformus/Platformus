// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Backend.ViewModels.Shared
{
  public class UserViewModelFactory : ViewModelFactoryBase
  {
    public UserViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public UserViewModel Create(User user)
    {
      return new UserViewModel()
      {
        Id = user.Id,
        Name = user.Name,
        Created = user.Created
      };
    }
  }
}