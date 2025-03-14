// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.Website.Data.Entities;

public class Property : IEntity<int>
{
  public int Id { get; set; }
  public int ObjectId { get; set; }
  public int MemberId { get; set; }
  public decimal? DecimalValue { get; set; }
  public string? StringValue { get; set; }

  public virtual Object? Object { get; set; }
  public virtual Member? Member { get; set; }
  public virtual ICollection<LocalizedProperty>? LocalizedProperties { get; set; }
}