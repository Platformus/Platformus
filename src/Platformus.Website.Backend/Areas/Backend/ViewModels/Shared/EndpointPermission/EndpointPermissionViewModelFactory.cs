// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Backend.ViewModels.Shared;
using Platformus.Core.Data.Entities;

namespace Platformus.Website.Backend.ViewModels.Shared
{
  public static class EndpointPermissionViewModelFactory
  {
    public static EndpointPermissionViewModel Create(Permission permission, bool isAssigned)
    {
      return new EndpointPermissionViewModel()
      {
        Permission = PermissionViewModelFactory.Create(permission),
        IsAssigned = isAssigned
      };
    }
  }
}