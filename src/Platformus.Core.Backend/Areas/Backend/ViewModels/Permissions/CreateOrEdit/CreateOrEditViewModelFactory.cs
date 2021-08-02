// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Data.Entities;

namespace Platformus.Core.Backend.ViewModels.Permissions
{
  public static class CreateOrEditViewModelFactory
  {
    public static CreateOrEditViewModel Create(Permission permission)
    {
      if (permission == null)
        return new CreateOrEditViewModel()
        {
        };

      return new CreateOrEditViewModel()
      {
        Id = permission.Id,
        Code = permission.Code,
        Name = permission.Name,
        Position = permission.Position
      };
    }
  }
}