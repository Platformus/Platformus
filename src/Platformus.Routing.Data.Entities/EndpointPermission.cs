// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Routing.Data.Entities
{
  public class EndpointPermission : IEntity
  {
    public int EndpointId { get; set; }
    public int PermissionId { get; set; }

    public virtual Endpoint Endpoint { get; set; }
    public virtual Permission Permission { get; set; }
  }
}