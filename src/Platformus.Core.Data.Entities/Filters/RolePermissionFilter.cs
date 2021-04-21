// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Core.Filters
{
  public class RolePermissionFilter : IFilter
  {
    public RoleFilter Role { get; set; }
    public PermissionFilter Permission { get; set; }

    public RolePermissionFilter() { }

    public RolePermissionFilter(RoleFilter role = null, PermissionFilter permission = null)
    {
      Role = role;
      Permission = permission;
    }
  }
}