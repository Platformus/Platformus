// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.Routing.Data.Abstractions;
using Platformus.Routing.Data.Entities;

namespace Platformus.Routing.Data.EntityFramework.PostgreSql
{
  public class EndpointRepository : RepositoryBase<Endpoint>, IEndpointRepository
  {
    public Endpoint WithKey(int id)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(e => e.Id == id);
    }

    public IEnumerable<Endpoint> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(e => e.Position);
    }

    public IEnumerable<Endpoint> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredEndpoints(dbSet.AsNoTracking(), filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    public void Create(Endpoint endpoint)
    {
      this.dbSet.Add(endpoint);
    }

    public void Edit(Endpoint endpoint)
    {
      this.storageContext.Entry(endpoint).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Endpoint endpoint)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM ""EndpointPermissions"" WHERE ""EndpointId"" = {0};
          DELETE FROM ""DataSources"" WHERE ""EndpointId"" = {0};
        ",
        endpoint.Id
      );

      this.dbSet.Remove(endpoint);
    }

    public int Count(string filter)
    {
      return this.GetFilteredEndpoints(dbSet, filter).Count();
    }

    private IQueryable<Endpoint> GetFilteredEndpoints(IQueryable<Endpoint> endpoints, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return endpoints;

      return endpoints.Where(e => e.Name.ToLower().Contains(filter.ToLower()));
    }
  }
}