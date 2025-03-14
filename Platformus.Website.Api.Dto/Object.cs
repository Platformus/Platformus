// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;

namespace Platformus.Website.Api.Dto;

[Magicalized("/api/v1/objects")]
[AuthenticatedOnly]
public class Object : IDto<Domain.Models.Object>
{
  public int Id { get; set; }
  public Class? Class { get; set; }
  public IEnumerable<Property>? Properties { get; set; }

  public Object() { }

  public Object(Domain.Models.Object _member)
  {
    this.Id = _member.Id;
    this.Class = _member.Class == null ? null : new Class(_member.Class);
    this.Properties = _member.Properties?.Select(p => new Property(p)).ToList();
  }

  public Domain.Models.Object ToModel()
  {
    return new Domain.Models.Object()
    {
      Id = this.Id,
      Class = this.Class?.ToModel(),
      Properties = this.Properties?.Select(p => p.ToModel()),
    };
  }
}