// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;

namespace Platformus.Website.Api.Dto;

[Magicalized("/api/v1/members")]
[AuthenticatedOnly]
public class Member : IDto<Domain.Models.Member>
{
  public int Id { get; set; }
  public Class? Class { get; set; }
  public MemberType? MemberType { get; set; }
  public string? Name { get; set; }
  public bool IsLocalizable { get; set; }
  public bool IsShownInList { get; set; }
  public string? CSharpName { get; set; }

  public Member() { }

  public Member(Domain.Models.Member _member)
  {
    this.Id = _member.Id;
    this.Class = _member.Class == null ? null : new Class(_member.Class);
    this.MemberType = _member.MemberType == null ? null : new MemberType(_member.MemberType);
    this.Name = _member.Name;
    this.IsLocalizable = _member.IsLocalizable;
    this.IsShownInList = _member.IsShownInList;
    this.CSharpName = _member.CSharpName;
  }

  public Domain.Models.Member ToModel()
  {
    return new Domain.Models.Member()
    {
      Id = this.Id,
      Class = this.Class?.ToModel(),
      MemberType = this.MemberType?.ToModel(),
      Name = this.Name,
      IsLocalizable = this.IsLocalizable,
      IsShownInList = this.IsShownInList,
      CSharpName = this.CSharpName
    };
  }
}