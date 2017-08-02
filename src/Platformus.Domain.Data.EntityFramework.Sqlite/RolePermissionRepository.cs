// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.EntityFramework.Sqlite
{
  public class MicrocontrollerPermissionRepository : RepositoryBase<MicrocontrollerPermission>, IMicrocontrollerPermissionRepository
  {
    public MicrocontrollerPermission WithKey(int microcontrollerId, int permissionId)
    {
      return this.dbSet.FirstOrDefault(mp => mp.MicrocontrollerId == microcontrollerId && mp.PermissionId == permissionId);
    }

    public IEnumerable<MicrocontrollerPermission> FilteredByMicrocontrollerId(int microcontrollerId)
    {
      return this.dbSet.Where(mp => mp.MicrocontrollerId == microcontrollerId);
    }

    public void Create(MicrocontrollerPermission microcontrollerPermission)
    {
      this.dbSet.Add(microcontrollerPermission);
    }

    public void Edit(MicrocontrollerPermission microcontrollerPermission)
    {
      this.storageContext.Entry(microcontrollerPermission).State = EntityState.Modified;
    }

    public void Delete(int microcontrollerId, int permissionId)
    {
      this.Delete(this.WithKey(microcontrollerId, permissionId));
    }

    public void Delete(MicrocontrollerPermission microcontrollerPermission)
    {
      this.dbSet.Remove(microcontrollerPermission);
    }
  }
}