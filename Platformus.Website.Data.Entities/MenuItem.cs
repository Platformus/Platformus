// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.Website.Data.Entities;

public class MenuItem : IEntity<int>
{
  public int Id { get; set; }
  public int MenuId { get; set; }

  public virtual Menu? Menu { get; set; }
  public virtual ICollection<LocalizedMenuItem>? LocalizedMenuItems { get; set; }
}