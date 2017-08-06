// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Routing.Data.Entities;

namespace Platformus.Routing.Data.Abstractions
{
  public interface IMicrocontrollerPermissionRepository : IRepository
  {
    MicrocontrollerPermission WithKey(int microcontrollerId, int permissionId);
    IEnumerable<MicrocontrollerPermission> FilteredByMicrocontrollerId(int microcontrollerId);
    void Create(MicrocontrollerPermission microcontrollerPermission);
    void Edit(MicrocontrollerPermission microcontrollerPermission);
    void Delete(int microcontrollerId, int permissionId);
    void Delete(MicrocontrollerPermission microcontrollerPermission);
  }
}