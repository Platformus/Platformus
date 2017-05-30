// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
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
  public class ObjectRepository : RepositoryBase<Object>, IObjectRepository
  {
    public Object WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(o => o.Id == id);
    }

    public Object WithUrl(string url)
    {
      return this.dbSet.FromSql(
        @"
          SELECT * FROM Objects WHERE Id IN
            (SELECT ObjectId FROM Properties WHERE MemberId IN
              (SELECT Id FROM Members WHERE Code = {0}) AND StringValueId IN (SELECT DictionaryId FROM Localizations WHERE Value = {1}))
        ",
        "Url", url
      ).FirstOrDefault();
    }

    public IEnumerable<Object> All()
    {
      return this.dbSet.OrderBy(o => o.Id);
    }

    public IEnumerable<Object> FilteredByClassId(int classId)
    {
      return this.dbSet.Where(o => o.ClassId == classId);
    }

    // TODO: must be changed!
    public IEnumerable<Object> FilteredByClassId(int classId, string storageDataType, int orderByMemberId, string direction, int cultureId)
    {
      return this.dbSet.FromSql(
        this.GetOrderedBySelectQuerySql("Objects.ClassId = {0}", storageDataType, orderByMemberId, direction, cultureId),
        classId
      );
    }

    public IEnumerable<Object> FilteredByClassIdRange(int classId, string orderBy, string direction, int skip, int take)
    {
      return this.dbSet.Where(o => o.ClassId == classId).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    public IEnumerable<Object> FilteredByClassIdAndObjectIdRange(int classId, int objectId, string orderBy, string direction, int skip, int take)
    {
      return this.dbSet.Where(o => o.ClassId == classId && o.ForeignRelations.Any(r => r.PrimaryId == objectId)).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    public IEnumerable<Object> Primary(int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM Objects WHERE Id IN (SELECT PrimaryId FROM Relations WHERE ForeignId = {0})", objectId);
    }

    // TODO: must be changed!
    public IEnumerable<Object> Primary(int objectId, string storageDataType, int orderByMemberId, string direction, int cultureId)
    {
      return this.dbSet.FromSql(
        this.GetOrderedBySelectQuerySql("Objects.Id IN (SELECT PrimaryId FROM Relations WHERE ForeignId = {0})", storageDataType, orderByMemberId, direction, cultureId),
        objectId
      );
    }

    public IEnumerable<Object> Primary(int memberId, int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM Objects WHERE Id IN (SELECT PrimaryId FROM Relations WHERE MemberId = {0} AND ForeignId = {1})", memberId, objectId);
    }

    // TODO: must be changed!
    public IEnumerable<Object> Primary(int memberId, int objectId, string storageDataType, int orderByMemberId, string direction, int cultureId)
    {
      return this.dbSet.FromSql(
        this.GetOrderedBySelectQuerySql("Objects.Id IN (SELECT PrimaryId FROM Relations WHERE MemberId = {0} AND ForeignId = {1})", storageDataType, orderByMemberId, direction, cultureId),
        memberId, objectId
      );
    }

    public IEnumerable<Object> Foreign(int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM Objects WHERE Id IN (SELECT ForeignId FROM Relations WHERE PrimaryId = {0})", objectId);
    }

    // TODO: must be changed!
    public IEnumerable<Object> Foreign(int objectId, string storageDataType, int orderByMemberId, string direction, int cultureId)
    {
      return this.dbSet.FromSql(
        this.GetOrderedBySelectQuerySql("Objects.Id IN (SELECT ForeignId FROM Relations WHERE PrimaryId = {0})", storageDataType, orderByMemberId, direction, cultureId),
        objectId
      );
    }

    public IEnumerable<Object> Foreign(int memberId, int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM Objects WHERE Id IN (SELECT ForeignId FROM Relations WHERE MemberId = {0} AND PrimaryId = {1})", memberId, objectId);
    }

    // TODO: must be changed!
    public IEnumerable<Object> Foreign(int memberId, int objectId, string storageDataType, int orderByMemberId, string direction, int cultureId)
    {
      return this.dbSet.FromSql(
        this.GetOrderedBySelectQuerySql("Objects.Id IN (SELECT ForeignId FROM Relations WHERE MemberId = {0} AND PrimaryId = {1})", storageDataType, orderByMemberId, direction, cultureId),
        memberId, objectId
      );
    }

    public void Create(Object @object)
    {
      this.dbSet.Add(@object);
    }

    public void Edit(Object @object)
    {
      this.storageContext.Entry(@object).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Object @object)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM SerializedObjects WHERE ObjectId = {0};
          CREATE TABLE #Dictionaries (Id INT PRIMARY KEY);
          INSERT INTO #Dictionaries SELECT StringValueId FROM Properties WHERE ObjectId = {0};
          DELETE FROM Properties WHERE ObjectId = {0};
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM #Dictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM #Dictionaries);
          DELETE FROM Relations WHERE PrimaryId = {0} OR ForeignId = {0};
        ",
        @object.Id
      );

      this.dbSet.Remove(@object);
    }

    public int CountByClassId(int classId)
    {
      return this.dbSet.Count(o => o.ClassId == classId);
    }

    private string GetOrderedBySelectQuerySql(string additionalWhereClause, string storageDataType, int orderByMemberId, string direction, int cultureId)
    {
      if (storageDataType == StorageDataType.Integer)
        return this.GetOrderedByIntegerValueSelectQuerySql(additionalWhereClause, orderByMemberId, direction);

      if (storageDataType == StorageDataType.Decimal)
        return this.GetOrderedByDecimalValueSelectQuerySql(additionalWhereClause, orderByMemberId, direction);

      if (storageDataType == StorageDataType.String)
        return this.GetOrderedByStringValueSelectQuerySql(additionalWhereClause, orderByMemberId, direction, cultureId);

      if (storageDataType == StorageDataType.DateTime)
        return this.GetOrderedByDateTimeValueSelectQuerySql(additionalWhereClause, orderByMemberId, direction);

      return null;
    }

    private string GetOrderedByIntegerValueSelectQuerySql(string additionalWhereClause, int orderByMemberId, string direction)
    {
      return
        "SELECT Objects.Id, Objects.ClassId FROM Objects " +
        "INNER JOIN Classes ON Classes.Id = Objects.ClassId " +
        "INNER JOIN Members ON Members.ClassId = Objects.ClassId OR Members.ClassId = Classes.ClassId " +
        "INNER JOIN Properties ON Properties.ObjectId = Objects.Id AND Properties.MemberId = Members.Id " +
        "WHERE " + additionalWhereClause + " AND Members.Id = \"" + orderByMemberId + "\" " +
        "ORDER BY Properties.IntegerValue " + direction;
    }

    private string GetOrderedByDecimalValueSelectQuerySql(string additionalWhereClause, int orderByMemberId, string direction)
    {
      return
        "SELECT Objects.Id, Objects.ClassId FROM Objects " +
        "INNER JOIN Classes ON Classes.Id = Objects.ClassId " +
        "INNER JOIN Members ON Members.ClassId = Objects.ClassId OR Members.ClassId = Classes.ClassId " +
        "INNER JOIN Properties ON Properties.ObjectId = Objects.Id AND Properties.MemberId = Members.Id " +
        "WHERE " + additionalWhereClause + " AND Members.Id = \"" + orderByMemberId + "\" " +
        "ORDER BY Properties.DecimalValue " + direction;
    }

    private string GetOrderedByStringValueSelectQuerySql(string additionalWhereClause, int orderByMemberId, string direction, int cultureId)
    {
      return
        "SELECT Objects.Id, Objects.ClassId FROM Objects " +
        "INNER JOIN Classes ON Classes.Id = Objects.ClassId " +
        "INNER JOIN Members ON Members.ClassId = Objects.ClassId OR Members.ClassId = Classes.ClassId " +
        "INNER JOIN Properties ON Properties.ObjectId = Objects.Id AND Properties.MemberId = Members.Id " +
        "INNER JOIN Localizations ON Localizations.DictionaryId = Properties.StringValueId " +
        "WHERE " + additionalWhereClause + " AND Members.Id = \"" + orderByMemberId + "\" AND Localizations.CultureId = " + cultureId + " " +
        "ORDER BY Localizations.Value " + direction;
    }

    private string GetOrderedByDateTimeValueSelectQuerySql(string additionalWhereClause, int orderByMemberId, string direction)
    {
      return
        "SELECT Objects.Id, Objects.ClassId FROM Objects " +
        "INNER JOIN Classes ON Classes.Id = Objects.ClassId " +
        "INNER JOIN Members ON Members.ClassId = Objects.ClassId OR Members.ClassId = Classes.ClassId " +
        "INNER JOIN Properties ON Properties.ObjectId = Objects.Id AND Properties.MemberId = Members.Id " +
        "WHERE " + additionalWhereClause + " AND Members.Id = \"" + orderByMemberId + "\" " +
        "ORDER BY Properties.DateTimeValue " + direction;
    }
  }
}