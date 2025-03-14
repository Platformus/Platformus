// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;

namespace Platformus.Website.Api.Dto;

[Magicalized("/api/v1/classes")]
public class Class : IDto<Domain.Models.Class>
{
  public int Id { get; set; }
  public string? SingularName { get; set; }
  public string? PluralName { get; set; }
  public string? UrlSegment { get; set; }
  public string? CSharpName { get; set; }
  public IEnumerable<Member>? Members { get; set; }
  public IEnumerable<Object>? Objects { get; set; }

  public Class() { }

  public Class(Domain.Models.Class _class)
  {
    this.Id = _class.Id;
    this.SingularName = _class.SingularName;
    this.PluralName = _class.PluralName;
    this.UrlSegment = _class.UrlSegment;
    this.CSharpName = _class.CSharpName;
    this.Members = _class.Members?.Select(m => new Member(m)).ToList();
    this.Objects = _class.Objects?.Select(o => new Object(o)).ToList();
  }

  public Domain.Models.Class ToModel()
  {
    return new Domain.Models.Class()
    {
      Id = Id,
      SingularName = SingularName,
      PluralName = PluralName,
      UrlSegment = UrlSegment,
      CSharpName = CSharpName
    };
  }
}
