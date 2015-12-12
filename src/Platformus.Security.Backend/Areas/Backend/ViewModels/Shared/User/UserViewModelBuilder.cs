// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Backend.ViewModels.Shared
{
  public class UserViewModelBuilder : ViewModelBuilderBase
  {
    public UserViewModelBuilder(IHandler handler)
      : base(handler)
    {
    }

    public UserViewModel Build(User user)
    {
      return new UserViewModel()
      {
        Id = user.Id,
        Name = user.Name,
        Created = user.Created.ToDateTime()
      };
    }
  }
}