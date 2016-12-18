// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.SqlServer;
using Microsoft.EntityFrameworkCore;
using Platformus.Configuration.Data.Abstractions;
using Platformus.Configuration.Data.Models;

namespace Platformus.Configuration.Data.EntityFramework.SqlServer
{
  public class VariableRepository : RepositoryBase<Variable>, IVariableRepository
  {
    public Variable WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(v => v.Id == id);
    }

    public Variable WithSectionIdAndCode(int sectionId, string code)
    {
      return this.dbSet.FirstOrDefault(v => v.SectionId == sectionId && string.Equals(v.Code, code, System.StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Variable> FilteredBySectionId(int sectionId)
    {
      return this.dbSet.Where(v => v.SectionId == sectionId).OrderBy(v => v.Position);
    }

    public void Create(Variable variable)
    {
      this.dbSet.Add(variable);
    }

    public void Edit(Variable variable)
    {
      this.storageContext.Entry(variable).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Variable variable)
    {
      this.dbSet.Remove(variable);
    }
  }
}