// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.Website.Data.Entities;

public class FieldType : IEntity<string>
{
  public string? Id { get; set; }
  public string? Name { get; set; }

  public virtual ICollection<Field>? Fields { get; set; }
}