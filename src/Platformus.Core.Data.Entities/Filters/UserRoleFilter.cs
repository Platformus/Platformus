// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Core.Filters
{
  public class UserRoleFilter : IFilter
  {
    public UserFilter User { get; set; }
    public RoleFilter Role { get; set; }

    public UserRoleFilter() { }

    public UserRoleFilter(UserFilter user = null, RoleFilter role = null)
    {
      User = user;
      Role = role;
    }
  }
}