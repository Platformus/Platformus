// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Data.Entities;

namespace Platformus.Core.Backend.ViewModels.Shared
{
  public class RoleViewModelFactory : ViewModelFactoryBase
  {
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