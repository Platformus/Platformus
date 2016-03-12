// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.PostgreSql;
using Microsoft.Data.Entity;
using Platformus.Configuration.Data.Abstractions;
using Platformus.Configuration.Data.Models;

namespace Platformus.Configuration.Data.EntityFramework.PostgreSql
{
  public class SectionRepository : RepositoryBase<Section>, ISectionRepository
  {
    public Section WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(s => s.Id == id);
    }

    public Section WithCode(string code)
    {
      return this.dbSet.FirstOrDefault(s => string.Equals(s.Code, code, System.StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Section> All()
    {
      return this.dbSet.OrderBy(s => s.Name);
    }

    public void Create(Section section)
    {
      this.dbSet.Add(section);
    }

    public void Edit(Section section)
    {
      this.dbContext.Entry(section).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Section section)
    {
      this.dbContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM Variables WHERE SectionId = {0};
        ",
        section.Id
      );

      this.dbSet.Remove(section);
    }
  }
}