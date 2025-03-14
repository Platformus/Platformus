// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Entities.Abstractions;
using Platformus.Core.Data.Entities;

namespace Platformus.Website.Data.Entities;

public class LocalizedProperty : IEntity<int, int>
{
  public int PropertyId { get; set; }
  public string? CultureId { get; set; }
  public string? StringValue { get; set; }

  public virtual Property? Property { get; set; }
  public virtual Culture? Culture { get; set; }
}