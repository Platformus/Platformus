// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;

namespace Platformus.Core.Api.Dto;

[Magicalized("/api/v1/permissions")]
[AuthenticatedOnly]
public class Permission : IDto<Domain.Models.Permission>
{
  public int Id { get; set; }
  public string? Code { get; set; }
  public string? Name { get; set; }
  public int? Position { get; set; }
  public IEnumerable<RolePermission>? RolePermissions { get; set; }

  public Permission() { }

  public Permission(Domain.Models.Permission _permission)
  {
    this.Id = _permission.Id;
    this.Code = _permission.Code;
    this.Name = _permission.Name;
    this.Position = _permission.Position;
    this.RolePermissions = _permission.RolePermissions?.Select(rp => new RolePermission(rp)).ToList();
  }

  public Domain.Models.Permission ToModel()
  {
    return new Domain.Models.Permission()
    {
      Id = this.Id,
      Code = this.Code,
      Name = this.Name,
      Position = this.Position
    };
  }
}