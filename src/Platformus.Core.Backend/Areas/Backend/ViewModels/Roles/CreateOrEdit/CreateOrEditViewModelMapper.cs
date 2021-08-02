// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Data.Entities;

namespace Platformus.Core.Backend.ViewModels.Roles
{
  public static class CreateOrEditViewModelMapper
  {
    public static Role Map(Role role, CreateOrEditViewModel createOrEdit)
    {
      role.Code = createOrEdit.Code;
      role.Name = createOrEdit.Name;
      role.Position = createOrEdit.Position;
      return role;
    }
  }
}