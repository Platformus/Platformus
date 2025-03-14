// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;

namespace Platformus.Website.Api.Dto;

[Magicalized("/api/v1/member-types", Magicalizer.Api.Dto.Abstractions.HttpMethod.Get)]
[AuthenticatedOnly]
public class MemberType : IDto<Domain.Models.MemberType>
{
  public string? Id { get; set; }
  public string? Name { get; set; }

  public MemberType() { }

  public MemberType(Domain.Models.MemberType _memberType)
  {
    this.Id = _memberType.Id;
    this.Name = _memberType.Name;
  }

  public Domain.Models.MemberType ToModel()
  {
    return new Domain.Models.MemberType()
    {
      Id = this.Id,
      Name = this.Name
    };
  }
}