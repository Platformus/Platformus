// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Entities.Abstractions;
using Platformus.Core.Data.Entities;

namespace Platformus.Website.Data.Entities;

public class LocalizedField : IEntity<int, int>
{
  public int FieldId { get; set; }
  public string? CultureId { get; set; }
  public string? Name { get; set; }

  public virtual Field? Field { get; set; }
  public virtual Culture? Culture { get; set; }
}