// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Routing.Data.Entities;

namespace Platformus.Routing.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="EndpointPermission"/> entities.
  /// </summary>
  public interface IEndpointPermissionRepository : IRepository
  {
    EndpointPermission WithKey(int endpointId, int permissionId);
    IEnumerable<EndpointPermission> FilteredByEndpointId(int endpointId);
    void Create(EndpointPermission endpointPermission);
    void Edit(EndpointPermission endpointPermission);
    void Delete(int endpointId, int permissionId);
    void Delete(EndpointPermission endpointPermission);
  }
}