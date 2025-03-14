// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.Core.Data.Entities;

public class UserRole : IEntity<int, int>
{
  public int UserId { get; set; }
  public int RoleId { get; set; }

  public virtual User? User { get; set; }
  public virtual Role? Role { get; set; }
}