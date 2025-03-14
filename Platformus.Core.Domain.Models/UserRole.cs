// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Core.Domain.Models;

public class UserRole : IModel<Data.Entities.UserRole, UserRoleFilter>
{
  public User? User { get; set; }
  public Role? Role { get; set; }

  public UserRole() { }

  public UserRole(Data.Entities.UserRole _userRole) : this(_userRole, mapUser: true, mapRole: true) { }

  public UserRole(Data.Entities.UserRole _userRole, bool mapUser = true, bool mapRole = true)
  {
    this.User = mapUser ? _userRole.User == null ? new User { Id = _userRole.UserId } : new User(_userRole.User, mapUserRoles: false) : null;
    this.Role = mapRole ? _userRole.Role == null ? new Role { Id = _userRole.RoleId } : new Role(_userRole.Role, mapUserRoles: false) : null;
  }

  public Data.Entities.UserRole ToEntity()
  {
    return new Data.Entities.UserRole()
    {
      UserId = this.User?.Id ?? 0,
      RoleId = this.Role?.Id ?? 0
    };
  }
}