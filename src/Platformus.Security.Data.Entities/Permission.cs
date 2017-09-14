// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Security.Data.Entities
{
  /// <summary>
  /// Represents a permission. The permissions are used to restrict access to the different web application resources.
  /// They can be grouped using the groups, and then assigned to a user.
  /// </summary>
  public class Permission : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the permission.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the permission. It is set by the user and might be used for the permission retrieval.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the permission name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the permission position. Position is used to sort the permission (smallest to largest).
    /// </summary>
    public int? Position { get; set; }
  }
}