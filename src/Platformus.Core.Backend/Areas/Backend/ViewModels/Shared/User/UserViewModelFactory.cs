// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Data.Entities;

namespace Platformus.Core.Backend.ViewModels.Shared
{
  public class UserViewModelFactory : ViewModelFactoryBase
  {
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