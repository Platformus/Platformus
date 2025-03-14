// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Website.Filters;

namespace Platformus.Website.Domain.Models;

public class Member : IModel<Data.Entities.Member, MemberFilter>
{
  public int Id { get; set; }
  public Class? Class { get; set; }
  public MemberType? MemberType { get; set; }
  public string? Name { get; set; }
  public bool IsLocalizable { get; set; }
  public bool IsShownInList { get; set; }
  public string? CSharpName { get; set; }
  public IEnumerable<Property>? Properties { get; set; }

  public Member() { }

  public Member(Data.Entities.Member _member) : this(_member, mapClass: true, mapMemberType: true, mapProperties: true) { }

  public Member(Data.Entities.Member _member, bool mapClass = true, bool mapMemberType = true, bool mapProperties = true)
  {
    this.Id = _member.Id;
    this.MemberType = mapMemberType ? _member.MemberType == null ? new MemberType { Id = _member.MemberTypeId } : new MemberType(_member.MemberType) : null;
    this.Class = mapClass ? _member.Class == null ? new Class { Id = _member.ClassId } : new Class(_member.Class, mapMembers: false) : null;
    this.Name = _member.Name;
    this.IsLocalizable = _member.IsLocalizable;
    this.IsShownInList = _member.IsShownInList;
    this.CSharpName = _member.CSharpName;
    this.Properties = mapProperties ? _member.Properties?.Select(p => new Property(p, mapObject: false)).ToList() : null;
  }

  public Data.Entities.Member ToEntity()
  {
    return new Data.Entities.Member()
    {
      Id = this.Id,
      ClassId = this.Class?.Id ?? 0,
      MemberTypeId = this.MemberType?.Id,
      Name = this.Name,
      IsLocalizable = this.IsLocalizable,
      IsShownInList = this.IsShownInList,
      CSharpName = this.CSharpName
    };
  }
}