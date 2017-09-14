// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.Security.Data.Entities
{
  /// <summary>
  /// Represents a role. The roles are used to group the permissions and assign them to the users.
  /// </summary>
  public class Role : IEntity
  {
    /// <summary>
    /// Gets or sets the unique identifier of the role.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique code of the role. It is set by the user and might be used for the role retrieval.
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Gets or sets the role name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the role position. Position is used to sort the roles (smallest to largest).
    /// </summary>
    public int? Position { get; set; }
  }
}