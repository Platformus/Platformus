// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.SqlServer;
using Microsoft.EntityFrameworkCore;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.Data.EntityFramework.SqlServer
{
  public class SerializedObjectRepository : RepositoryBase<SerializedObject>, ISerializedObjectRepository
  {
    public SerializedObject WithKey(int cultureId, int objectId)
    {
      return this.dbSet.FirstOrDefault(so => so.CultureId == cultureId && so.ObjectId == objectId);
    }

    public SerializedObject WithCultureIdAndUrlPropertyStringValue(int cultureId, string urlPropertyStringValue)
    {
      return this.dbSet.FirstOrDefault(so => so.CultureId == cultureId && string.Equals(so.UrlPropertyStringValue, urlPropertyStringValue, System.StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<SerializedObject> FilteredByCultureId(int cultureId)
    {
      return this.dbSet.Where(so => so.CultureId == cultureId).OrderBy(so => so.UrlPropertyStringValue);
    }

    public IEnumerable<SerializedObject> FilteredByClassId(int classId)
    {
      return this.dbSet.FromSql("SELECT * FROM SerializedObjects WHERE ObjectId IN (SELECT Id FROM Objects WHERE ClassId = {0})", classId);
    }

    public IEnumerable<SerializedObject> Primary(int cultureId, int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM SerializedObjects WHERE CultureId = {0} AND ObjectId IN (SELECT PrimaryId FROM Relations WHERE ForeignId = {1})", cultureId, objectId);
    }

    public IEnumerable<SerializedObject> Primary(int cultureId, int memberId, int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM SerializedObjects WHERE CultureId = {0} AND ObjectId IN (SELECT PrimaryId FROM Relations WHERE MemberId = {1} AND ForeignId = {2})", cultureId, memberId, objectId);
    }

    public IEnumerable<SerializedObject> Foreign(int cultureId, int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM SerializedObjects WHERE CultureId = {0} AND ObjectId IN (SELECT ForeignId FROM Relations WHERE PrimaryId = {1})", cultureId, objectId);
    }

    public IEnumerable<SerializedObject> Foreign(int cultureId, int memberId, int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM SerializedObjects WHERE CultureId = {0} AND ObjectId IN (SELECT ForeignId FROM Relations WHERE MemberId = {1} AND PrimaryId = {2})", cultureId, memberId, objectId);
    }

    public void Create(SerializedObject serializedObject)
    {
      this.dbSet.Add(serializedObject);
    }

    public void Edit(SerializedObject serializedObject)
    {
      this.storageContext.Entry(serializedObject).State = EntityState.Modified;
    }

    public void Delete(int cultureId, int objectId)
    {
      this.Delete(this.WithKey(cultureId, objectId));
    }

    public void Delete(SerializedObject serializedObject)
    {
      this.dbSet.Remove(serializedObject);
    }
  }
}