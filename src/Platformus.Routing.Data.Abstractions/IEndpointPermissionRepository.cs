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
    /// <summary>
    /// Gets the endpoint permission by the endpoint identifier and permission identifier.
    /// </summary>
    /// <param name="endpointId">The unique identifier of the endpoint this endpoint permission is related to.</param>
    /// <param name="permissionId">The unique identifier of the permission this endpoint permission is related to.</param>
    /// <returns>Found endpoint permission with the given endpoint identifier and permission identifier.</returns>
    EndpointPermission WithKey(int endpointId, int permissionId);

    /// <summary>
    /// Gets the endpoint permissions filtered by the endpoint identifier.
    /// </summary>
    /// <param name="endpointId">The unique identifier of the endpoint these endpoint permissions belongs to.</param>
    /// <returns>Found endpoint permissions.</returns>
    IEnumerable<EndpointPermission> FilteredByEndpointId(int endpointId);

    /// <summary>
    /// Creates the endpoint permission.
    /// </summary>
    /// <param name="endpointPermission">The endpoint permission to create.</param>
    void Create(EndpointPermission endpointPermission);

    /// <summary>
    /// Edits the endpoint permission.
    /// </summary>
    /// <param name="endpointPermission">The endpoint permission to edit.</param>
    void Edit(EndpointPermission endpointPermission);

    /// <summary>
    /// Deletes the endpoint permission specified by the endpoint identifier and permission identifier.
    /// </summary>
    /// <param name="endpointId">The unique identifier of the endpoint this endpoint permission is related to.</param>
    /// <param name="permissionId">The unique identifier of the permission this endpoint permission is related to.</param>
    void Delete(int endpointId, int permissionId);

    /// <summary>
    /// Deletes the endpoint permission.
    /// </summary>
    /// <param name="endpointPermission">The endpoint permission to delete.</param>
    void Delete(EndpointPermission endpointPermission);
  }
}