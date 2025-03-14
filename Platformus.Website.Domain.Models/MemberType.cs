// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Domain.Models.Abstractions;
using Platformus.Website.Filters;

namespace Platformus.Website.Domain.Models;

public class MemberType : IModel<Data.Entities.MemberType, MemberTypeFilter>
{
  public string? Id { get; set; }
  public string? Name { get; set; }

  public MemberType() { }

  public MemberType(Data.Entities.MemberType _memberType)
  {
    this.Id = _memberType.Id;
    this.Name = _memberType.Name;
  }

  public Data.Entities.MemberType ToEntity()
  {
    return new Data.Entities.MemberType
    {
      Id = this.Id,
      Name = this.Name
    };
  }
}