// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.SqlServer;
using Microsoft.EntityFrameworkCore;
using Platformus.Content.Data.Abstractions;
using Platformus.Content.Data.Models;

namespace Platformus.Content.Data.EntityFramework.SqlServer
{
  public class CachedObjectRepository : RepositoryBase<CachedObject>, ICachedObjectRepository
  {
    public CachedObject WithKey(int cultureId, int objectId)
    {
      return this.dbSet.FirstOrDefault(co => co.CultureId == cultureId && co.ObjectId == objectId);
    }

    public CachedObject WithCultureIdAndUrl(int cultureId, string url)
    {
      return this.dbSet.FirstOrDefault(co => co.CultureId == cultureId && string.Equals(co.Url, url, System.StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<CachedObject> FilteredByCultureId(int cultureId)
    {
      return this.dbSet.Where(co => co.CultureId == cultureId).OrderBy(co => co.Url);
    }

    public IEnumerable<CachedObject> Primary(int cultureId, int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM CachedObjects WHERE CultureId = {0} AND ObjectId IN (SELECT PrimaryId FROM Relations WHERE ForeignId = {1})", cultureId, objectId);
    }

    public IEnumerable<CachedObject> Primary(int cultureId, int memberId, int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM CachedObjects WHERE CultureId = {0} AND ObjectId IN (SELECT PrimaryId FROM Relations WHERE MemberId = {1} AND ForeignId = {2})", cultureId, memberId, objectId);
    }

    public IEnumerable<CachedObject> Foreign(int cultureId, int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM CachedObjects WHERE CultureId = {0} AND ObjectId IN (SELECT ForeignId FROM Relations WHERE PrimaryId = {1})", cultureId, objectId);
    }

    public IEnumerable<CachedObject> Foreign(int cultureId, int memberId, int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM CachedObjects WHERE CultureId = {0} AND ObjectId IN (SELECT ForeignId FROM Relations WHERE MemberId = {1} AND PrimaryId = {2})", cultureId, memberId, objectId);
    }

    public void Create(CachedObject cachedObject)
    {
      this.dbSet.Add(cachedObject);
    }

    public void Edit(CachedObject cachedObject)
    {
      this.dbContext.Entry(cachedObject).State = EntityState.Modified;
    }

    public void Delete(int cultureId, int objectId)
    {
      this.Delete(this.WithKey(cultureId, objectId));
    }

    public void Delete(CachedObject cachedObject)
    {
      this.dbSet.Remove(cachedObject);
    }
  }
}