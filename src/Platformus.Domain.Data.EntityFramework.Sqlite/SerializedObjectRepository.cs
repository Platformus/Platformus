// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework.Sqlite;
using Microsoft.EntityFrameworkCore;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.Data.EntityFramework.Sqlite
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

    public IEnumerable<SerializedObject> FilteredByClassId(int cultureId, int classId)
    {
      return this.dbSet.FromSql("SELECT * FROM SerializedObjects WHERE CultureId = {0} AND ObjectId IN (SELECT Id FROM Objects WHERE ClassId = {1})", cultureId, classId);
    }

    // TODO: must be changed!
    public IEnumerable<SerializedObject> FilteredByClassId(int cultureId, int classId, string storageDataType, int orderByMemberId, string direction)
    {
      return this.dbSet.FromSql(
        this.GetOrderedBySelectQuerySql("SerializedObjects.CultureId = {0} AND SerializedObjects.ObjectId IN (SELECT Id FROM Objects WHERE ClassId = {1})", storageDataType, orderByMemberId, direction, cultureId),
        cultureId, classId
      );
    }

    public IEnumerable<SerializedObject> Primary(int cultureId, int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM SerializedObjects WHERE CultureId = {0} AND ObjectId IN (SELECT PrimaryId FROM Relations WHERE ForeignId = {1})", cultureId, objectId);
    }

    // TODO: must be changed!
    public IEnumerable<SerializedObject> Primary(int cultureId, int objectId, string storageDataType, int orderByMemberId, string direction)
    {
      return this.dbSet.FromSql(
        this.GetOrderedBySelectQuerySql("SerializedObjects.CultureId = {0} AND SerializedObjects.ObjectId IN (SELECT PrimaryId FROM Relations WHERE ForeignId = {1})", storageDataType, orderByMemberId, direction, cultureId),
        cultureId, objectId
      );
    }

    public IEnumerable<SerializedObject> Primary(int cultureId, int memberId, int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM SerializedObjects WHERE CultureId = {0} AND ObjectId IN (SELECT PrimaryId FROM Relations WHERE MemberId = {1} AND ForeignId = {2})", cultureId, memberId, objectId);
    }

    // TODO: must be changed!
    public IEnumerable<SerializedObject> Primary(int cultureId, int memberId, int objectId, string storageDataType, int orderByMemberId, string direction)
    {
      return this.dbSet.FromSql(
        this.GetOrderedBySelectQuerySql("SerializedObjects.CultureId = {0} AND SerializedObjects.ObjectId IN (SELECT PrimaryId FROM Relations WHERE MemberId = {1} AND ForeignId = {2})", storageDataType, orderByMemberId, direction, cultureId),
        cultureId, memberId, objectId
      );
    }

    public IEnumerable<SerializedObject> Foreign(int cultureId, int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM SerializedObjects WHERE CultureId = {0} AND ObjectId IN (SELECT ForeignId FROM Relations WHERE PrimaryId = {1})", cultureId, objectId);
    }

    // TODO: must be changed!
    public IEnumerable<SerializedObject> Foreign(int cultureId, int objectId, string storageDataType, int orderByMemberId, string direction)
    {
      return this.dbSet.FromSql(
        this.GetOrderedBySelectQuerySql("SerializedObjects.CultureId = {0} AND SerializedObjects.ObjectId IN (SELECT ForeignId FROM Relations WHERE PrimaryId = {1})", storageDataType, orderByMemberId, direction, cultureId),
        cultureId, objectId
      );
    }

    public IEnumerable<SerializedObject> Foreign(int cultureId, int memberId, int objectId)
    {
      return this.dbSet.FromSql("SELECT * FROM SerializedObjects WHERE CultureId = {0} AND ObjectId IN (SELECT ForeignId FROM Relations WHERE MemberId = {1} AND PrimaryId = {2})", cultureId, memberId, objectId);
    }

    // TODO: must be changed!
    public IEnumerable<SerializedObject> Foreign(int cultureId, int memberId, int objectId, string storageDataType, int orderByMemberId, string direction)
    {
      return this.dbSet.FromSql(
        this.GetOrderedBySelectQuerySql("SerializedObjects.CultureId = {0} AND SerializedObjects.ObjectId IN (SELECT ForeignId FROM Relations WHERE MemberId = {1} AND PrimaryId = {2})", storageDataType, orderByMemberId, direction, cultureId),
        cultureId, memberId, objectId
      );
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
        "SELECT SerializedObjects.CultureId, SerializedObjects.ObjectId, SerializedObjects.UrlPropertyStringValue, SerializedObjects.SerializedProperties FROM SerializedObjects " +
        "INNER JOIN Objects ON Objects.Id = SerializedObjects.ObjectId " +
        "INNER JOIN Classes ON Classes.Id = Objects.ClassId " +
        "INNER JOIN Members ON Members.ClassId = Objects.ClassId OR Members.ClassId = Classes.ClassId " +
        "INNER JOIN Properties ON Properties.ObjectId = Objects.Id AND Properties.MemberId = Members.Id " +
        "WHERE " + additionalWhereClause + " AND Members.Id = '" + orderByMemberId + "' " +
        "ORDER BY Properties.IntegerValue " + direction;
    }

    private string GetOrderedByDecimalValueSelectQuerySql(string additionalWhereClause, int orderByMemberId, string direction)
    {
      return
        "SELECT SerializedObjects.CultureId, SerializedObjects.ObjectId, SerializedObjects.UrlPropertyStringValue, SerializedObjects.SerializedProperties FROM SerializedObjects " +
        "INNER JOIN Objects ON Objects.Id = SerializedObjects.ObjectId " +
        "INNER JOIN Classes ON Classes.Id = Objects.ClassId " +
        "INNER JOIN Members ON Members.ClassId = Objects.ClassId OR Members.ClassId = Classes.ClassId " +
        "INNER JOIN Properties ON Properties.ObjectId = Objects.Id AND Properties.MemberId = Members.Id " +
        "WHERE " + additionalWhereClause + " AND Members.Id = '" + orderByMemberId + "' " +
        "ORDER BY Properties.DecimalValue " + direction;
    }

    private string GetOrderedByStringValueSelectQuerySql(string additionalWhereClause, int orderByMemberId, string direction, int cultureId)
    {
      return
        "SELECT SerializedObjects.CultureId, SerializedObjects.ObjectId, SerializedObjects.UrlPropertyStringValue, SerializedObjects.SerializedProperties FROM SerializedObjects " +
        "INNER JOIN Objects ON Objects.Id = SerializedObjects.ObjectId " +
        "INNER JOIN Classes ON Classes.Id = Objects.ClassId " +
        "INNER JOIN Members ON Members.ClassId = Objects.ClassId OR Members.ClassId = Classes.ClassId " +
        "INNER JOIN Properties ON Properties.ObjectId = Objects.Id AND Properties.MemberId = Members.Id " +
        "INNER JOIN Localizations ON Localizations.DictionaryId = Properties.StringValueId " +
        "WHERE " + additionalWhereClause + " AND Members.Id = '" + orderByMemberId + "' AND Localizations.CultureId = " + cultureId + " " +
        "ORDER BY Localizations.Value " + direction;
    }

    private string GetOrderedByDateTimeValueSelectQuerySql(string additionalWhereClause, int orderByMemberId, string direction)
    {
      return
        "SELECT SerializedObjects.CultureId, SerializedObjects.ObjectId, SerializedObjects.UrlPropertyStringValue, SerializedObjects.SerializedProperties FROM SerializedObjects " +
        "INNER JOIN Objects ON Objects.Id = SerializedObjects.ObjectId " +
        "INNER JOIN Classes ON Classes.Id = Objects.ClassId " +
        "INNER JOIN Members ON Members.ClassId = Objects.ClassId OR Members.ClassId = Classes.ClassId " +
        "INNER JOIN Properties ON Properties.ObjectId = Objects.Id AND Properties.MemberId = Members.Id " +
        "WHERE " + additionalWhereClause + " AND Members.Id = '" + orderByMemberId + "' " +
        "ORDER BY datetime(Properties.DateTimeValue) " + direction;
    }
  }
}