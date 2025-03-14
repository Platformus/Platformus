// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Website.Filters;

namespace Platformus.Website.Domain.Models;

public class Class : IModel<Data.Entities.Class, ClassFilter>
{
  public int Id { get; set; }
  public string? SingularName { get; set; }
  public string? PluralName { get; set; }
  public string? UrlSegment { get; set; }
  public string? CSharpName { get; set; }
  public IEnumerable<Member>? Members { get; set; }
  public IEnumerable<Object>? Objects { get; set; }

  public Class() { }

  public Class(Data.Entities.Class _class) : this(_class, mapMembers: true, mapObjects: true) { }

  public Class(Data.Entities.Class _class, bool mapMembers = true, bool mapObjects = true)
  {
    this.Id = _class.Id;
    this.SingularName = _class.SingularName;
    this.PluralName = _class.PluralName;
    this.UrlSegment = _class.UrlSegment;
    this.CSharpName = _class.CSharpName;
    this.Members = mapMembers ? _class.Members?.Select(m => new Member(m, mapClass: false)).ToList() : null;
    this.Objects = mapObjects ? _class.Objects?.Select(o => new Object(o, mapClass: false)).ToList() : null;
  }

  public Data.Entities.Class ToEntity()
  {
    return new Data.Entities.Class()
    {
      Id = this.Id,
      SingularName = this.SingularName,
      PluralName = this.PluralName,
      UrlSegment = this.UrlSegment,
      CSharpName = this.CSharpName
    };
  }
}