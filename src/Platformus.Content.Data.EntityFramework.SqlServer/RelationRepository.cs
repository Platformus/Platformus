// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.SqlServer;
using Microsoft.Data.Entity;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;

namespace Platformus.Content.Data.EntityFramework.SqlServer
{
  public class RelationRepository : RepositoryBase<Relation>, IRelationRepository
  {
    public Relation WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(r => r.Id == id);
    }

    public IEnumerable<Relation> FilteredByPrimaryId(int primaryId)
    {
      return this.dbSet.Where(r => r.PrimaryId == primaryId);
    }

    public IEnumerable<Relation> FilteredByForeignId(int foreignId)
    {
      return this.dbSet.Where(r => r.ForeignId == foreignId);
    }

    public IEnumerable<Relation> FilteredByMemberIdAndForeignId(int memberId, int foreignId)
    {
      return this.dbSet.Where(r => r.MemberId == memberId && r.ForeignId == foreignId);
    }

    public void Create(Relation relation)
    {
      this.dbSet.Add(relation);
    }

    public void Edit(Relation relation)
    {
      this.dbContext.Entry(relation).State = EntityState.Modified;
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