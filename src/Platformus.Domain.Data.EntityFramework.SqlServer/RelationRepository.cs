// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.EntityFramework.SqlServer
{
  public class RelationRepository : RepositoryBase<Relation>, IRelationRepository
  {
    public Relation WithKey(int id)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(r => r.Id == id);
    }

    public IEnumerable<Relation> FilteredByPrimaryId(int primaryId)
    {
      return this.dbSet.AsNoTracking().Where(r => r.PrimaryId == primaryId);
    }

    public IEnumerable<Relation> FilteredByForeignId(int foreignId)
    {
      return this.dbSet.AsNoTracking().Where(r => r.ForeignId == foreignId);
    }

    public IEnumerable<Relation> FilteredByMemberIdAndForeignId(int memberId, int foreignId)
    {
      return this.dbSet.AsNoTracking().Where(r => r.MemberId == memberId && r.ForeignId == foreignId);
    }

    public void Create(Relation relation)
    {
      this.dbSet.Add(relation);
    }

    public void Edit(Relation relation)
    {
      this.storageContext.Entry(relation).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Relation relation)
    {
      this.dbSet.Remove(relation);
    }
  }
}