// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Data.Entities;

namespace Platformus.Core.Backend.ViewModels.Permissions;

public static class CreateOrEditViewModelMapper
{
  public static Permission Map(Permission permission, CreateOrEditViewModel createOrEdit)
  {
    permission.Code = createOrEdit.Code;
    permission.Name = createOrEdit.Name;
    permission.Position = createOrEdit.Position;
    return permission;
  }
}