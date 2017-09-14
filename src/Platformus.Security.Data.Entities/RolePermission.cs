// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Security.Data.Entities
{
  /// <summary>
  /// Represents a many-to-many relationship between the roles and permissions.
  /// </summary>
  public class RolePermission : IEntity
  {
    /// <summary>
    /// Gets or sets the role identifier this role permission is related to.
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    /// Gets or sets the permission identifier this role permission is related to.
    /// </summary>
    public int PermissionId { get; set; }

    public virtual Role Role { get; set; }
    public virtual Permission Permission { get; set; }
  }
}