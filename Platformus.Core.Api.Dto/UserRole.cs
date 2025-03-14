// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;

namespace Platformus.Core.Api.Dto;

[Magicalized("/api/v1/user-roles")]
[AuthenticatedOnly]
public class UserRole : IDto<Domain.Models.UserRole>
{
  public User? User { get; set; }
  public Role? Role { get; set; }

  public UserRole() { }

  public UserRole(Domain.Models.UserRole _userRole)
  {
    this.User = _userRole.User == null ? null : new User(_userRole.User);
    this.Role = _userRole.Role == null ? null : new Role(_userRole.Role);
  }

  public Domain.Models.UserRole ToModel()
  {
    return new Domain.Models.UserRole()
    {
      User = this.User?.ToModel(),
      Role = this.Role?.ToModel()
    };
  }
}