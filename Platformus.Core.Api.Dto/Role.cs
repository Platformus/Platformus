// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;
using HttpMethod = Magicalizer.Api.Dto.Abstractions.HttpMethod;

namespace Platformus.Core.Api.Dto;

[Magicalized("/api/v1/roles")]
[AuthorizedOnly($"{nameof(Role)}.{nameof(HttpMethod.Get)}", HttpMethod.Get)]
[AuthorizedOnly($"{nameof(Role)}.{nameof(HttpMethod.Post)}", HttpMethod.Post)]
[AuthorizedOnly($"{nameof(Role)}.{nameof(HttpMethod.Put)}", HttpMethod.Put)]
[AuthorizedOnly($"{nameof(Role)}.{nameof(HttpMethod.Patch)}", HttpMethod.Patch)]
[AuthorizedOnly($"{nameof(Role)}.{nameof(HttpMethod.Delete)}", HttpMethod.Delete)]
public class Role : IDto<Domain.Models.Role>
{
  public int Id { get; set; }
  public string? Code { get; set; }
  public string? Name { get; set; }
  public int? Position { get; set; }
  public IEnumerable<UserRole>? UserRoles { get; set; }
  public IEnumerable<RolePermission>? RolePermissions { get; set; }

  public Role() { }

  public Role(Domain.Models.Role _role)
  {
    this.Id = _role.Id;
    this.Code = _role.Code;
    this.Name = _role.Name;
    this.Position = _role.Position;
    this.UserRoles = _role.UserRoles?.Select(ur => new UserRole(ur)).ToList();
    this.RolePermissions = _role.RolePermissions?.Select(rp => new RolePermission(rp)).ToList();
  }

  public Domain.Models.Role ToModel()
  {
    return new Domain.Models.Role()
    {
      Id = this.Id,
      Code = this.Code,
      Name = this.Name,
      Position = this.Position
    };
  }
}