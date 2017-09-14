// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Routing.Data.Entities
{
  /// <summary>
  /// Represents a many-to-many relationship between the endpoints and permissions.
  /// </summary>
  public class EndpointPermission : IEntity
  {
    /// <summary>
    /// Gets or sets the endpoint identifier this endpoint permission is related to.
    /// </summary>
    public int EndpointId { get; set; }

    /// <summary>
    /// Gets or sets the permission identifier this endpoint permission is related to.
    /// </summary>
    public int PermissionId { get; set; }

    public virtual Endpoint Endpoint { get; set; }
    public virtual Permission Permission { get; set; }
  }
}