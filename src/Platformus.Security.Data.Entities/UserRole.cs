// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Security.Data.Entities
{
  /// <summary>
  /// Represents a many-to-many relationship between the users and roles.
  /// </summary>
  public class UserRole : IEntity
  {
    /// <summary>
    /// Gets or sets the user identifier this user role is related to.
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Gets or sets the role identifier this user role is related to.
    /// </summary>
    public int RoleId { get; set; }

    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
  }
}