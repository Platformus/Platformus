// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Data.Entities;

namespace Platformus.Core.Backend.ViewModels.Shared;

public static class PermissionViewModelFactory
{
  public static PermissionViewModel Create(Permission permission)
  {
    return new PermissionViewModel()
    {
      Id = permission.Id,
      Name = permission.Name,
      Position = permission.Position
    };
  }
}