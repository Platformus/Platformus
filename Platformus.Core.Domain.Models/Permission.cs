// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Core.Filters;

namespace Platformus.Core.Domain.Models;

public class Permission : IModel<Data.Entities.Permission, PermissionFilter>
{
  public int Id { get; set; }
  public string? Code { get; set; }
  public string? Name { get; set; }
  public int? Position { get; set; }
  public IEnumerable<RolePermission>? RolePermissions { get; set; }

  public Permission() { }

  public Permission(Data.Entities.Permission _permission) : this(_permission, mapRolePermissions: true) { }

  public Permission(Data.Entities.Permission _permission, bool mapRolePermissions = true)
  {
    this.Id = _permission.Id;
    this.Code = _permission.Code;
    this.Name = _permission.Name;
    this.Position = _permission.Position;
    this.RolePermissions = mapRolePermissions ? _permission.RolePermissions?.Select(rp => new RolePermission(rp, mapPermission: false)).ToList() : null;
  }

  public Data.Entities.Permission ToEntity()
  {
    return new Data.Entities.Permission()
    {
      Id = this.Id,
      Code = this.Code,
      Name = this.Name,
      Position = this.Position
    };
  }
}