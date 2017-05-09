// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.SqlServer;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.Data.EntityFramework.SqlServer
{
  public class MicrocontrollerRepository : RepositoryBase<Microcontroller>, IMicrocontrollerRepository
  {
    public Microcontroller WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(m => m.Id == id);
    }

    public IEnumerable<Microcontroller> All()
    {
      return this.dbSet.OrderBy(m => m.Position);
    }

    public IEnumerable<Microcontroller> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredMicrocontrollers(dbSet, filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    public void Create(Microcontroller microcontroller)
    {
      this.dbSet.Add(microcontroller);
    }

    public void Edit(Microcontroller microcontroller)
    {
      this.storageContext.Entry(microcontroller).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Microcontroller microcontroller)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM DataSources WHERE MicrocontrollerId = {0};
        ",
        microcontroller.Id
      );

      this.dbSet.Remove(microcontroller);
    }

    public int Count(string filter)
    {
      return this.GetFilteredMicrocontrollers(dbSet, filter).Count();
    }

    private IQueryable<Microcontroller> GetFilteredMicrocontrollers(IQueryable<Microcontroller> microcontrollers, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return microcontrollers;

      return microcontrollers.Where(m => m.Name.ToLower().Contains(filter.ToLower()));
    }
  }
}