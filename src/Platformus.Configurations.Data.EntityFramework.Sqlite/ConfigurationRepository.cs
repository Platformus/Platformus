// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.Sqlite;
using Microsoft.EntityFrameworkCore;
using Platformus.Configurations.Data.Abstractions;
using Platformus.Configurations.Data.Models;

namespace Platformus.Configurations.Data.EntityFramework.Sqlite
{
  public class ConfigurationRepository : RepositoryBase<Configuration>, IConfigurationRepository
  {
    public Configuration WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(c => c.Id == id);
    }

    public Configuration WithCode(string code)
    {
      return this.dbSet.FirstOrDefault(c => string.Equals(c.Code, code, System.StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Configuration> All()
    {
      return this.dbSet.OrderBy(c => c.Name);
    }

    public void Create(Configuration configuration)
    {
      this.dbSet.Add(configuration);
    }

    public void Edit(Configuration configuration)
    {
      this.storageContext.Entry(configuration).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Configuration configuration)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM Variables WHERE ConfigurationId = {0};
        ",
        configuration.Id
      );

      this.dbSet.Remove(configuration);
    }
  }
}