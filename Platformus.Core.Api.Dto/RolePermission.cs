// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;
using HttpMethod = Magicalizer.Api.Dto.Abstractions.HttpMethod;

namespace Platformus.Core.Api.Dto;

[Magicalized("/api/v1/role-permissions")]
[AuthorizedOnly($"{nameof(RolePermission)}.{nameof(HttpMethod.Get)}", HttpMethod.Get)]
[AuthorizedOnly($"{nameof(RolePermission)}.{nameof(HttpMethod.Post)}", HttpMethod.Post)]
[AuthorizedOnly($"{nameof(RolePermission)}.{nameof(HttpMethod.Put)}", HttpMethod.Put)]
[AuthorizedOnly($"{nameof(RolePermission)}.{nameof(HttpMethod.Patch)}", HttpMethod.Patch)]
[AuthorizedOnly($"{nameof(RolePermission)}.{nameof(HttpMethod.Delete)}", HttpMethod.Delete)]
public class RolePermission : IDto<Domain.Models.RolePermission>
{
  public Role? Role { get; set; }
  public Permission? Permission { get; set; }

  public RolePermission() { }

  public RolePermission(Domain.Models.RolePermission _rolePermission)
  {
    this.Role = _rolePermission.Role == null ? null : new Role(_rolePermission.Role);
    this.Permission = _rolePermission.Permission == null ? null : new Permission(_rolePermission.Permission);
  }

  public Domain.Models.RolePermission ToModel()
  {
    return new Domain.Models.RolePermission()
    {
      Role = this.Role?.ToModel(),
      Permission = this.Permission?.ToModel()
    };
  }
}