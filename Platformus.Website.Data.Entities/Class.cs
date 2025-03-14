// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.Website.Data.Entities;

public class Class : IEntity<int>
{
  public int Id { get; set; }
  public string? SingularName { get; set; }
  public string? PluralName { get; set; }
  public string? UrlSegment { get; set; }
  public string? CSharpName { get; set; }

  public virtual ICollection<Member>? Members { get; set; }
  public virtual ICollection<Object>? Objects { get; set; }
}