// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.Website.Data.Entities;

public class Object : IEntity<int>
{
  public int Id { get; set; }
  public int ClassId { get; set; }

  public virtual Class? Class { get; set; }
  public virtual ICollection<Property>? Properties { get; set; }
}