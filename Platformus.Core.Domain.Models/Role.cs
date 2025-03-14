// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Core.Domain.Models;

public class Role : IModel<Data.Entities.Role, RoleFilter>
{
  public int Id { get; set; }
  public string? Code { get; set; }
  public string? Name { get; set; }
  public int? Position { get; set; }
  public IEnumerable<UserRole>? UserRoles { get; set; }
  public IEnumerable<RolePermission>? RolePermissions { get; set; }

  public Role() { }

  public Role(Data.Entities.Role _role) : this(_role, mapUserRoles: true, mapRolePermissions: true) { }

  public Role(Data.Entities.Role _role, bool mapUserRoles = true, bool mapRolePermissions = true)
  {
    this.Id = _role.Id;
    this.Code = _role.Code;
    this.Name = _role.Name;
    this.Position = _role.Position;
    this.UserRoles = mapUserRoles ? _role.UserRoles?.Select(ur => new UserRole(ur, mapRole: false)).ToList() : null;
    this.RolePermissions = mapRolePermissions ? _role.RolePermissions?.Select(rp => new RolePermission(rp, mapRole: false)).ToList() : null;
  }

  public Data.Entities.Role ToEntity()
  {
    return new Data.Entities.Role()
    {
      Id = this.Id,
      Code = this.Code,
      Name = this.Name,
      Position = this.Position
    };
  }
}