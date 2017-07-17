// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

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

    public IEnumerable<SerializedObject> FilteredByCultureIdAndClassId(int cultureId, int classId)
    {
      return this.dbSet.Where(so => so.CultureId == cultureId && so.ClassId == classId);
    }

    public IEnumerable<SerializedObject> FilteredByCultureIdAndClassId(int cultureId, int classId, Params @params)
    {
      IQueryable<SerializedObject> results = this.dbSet.FromSql(
        this.GetSortedSelectQuerySql("SerializedObjects.ClassId = {1}", @params),
        cultureId, classId
      );

      return this.ApplyPaging(results, @params);
    }

    public IEnumerable<SerializedObject> FilteredByCultureIdAndClassIdAndObjectId(int cultureId, int classId, int objectId, Params @params)
    {
      IQueryable<SerializedObject> results = this.dbSet.FromSql(
        this.GetSortedSelectQuerySql("SerializedObjects.ClassId = {1} AND SerializedObjects.ObjectId IN (SELECT ForeignId FROM Relations WHERE PrimaryId = {2})", @params),
        cultureId, classId, objectId
      );

      return this.ApplyPaging(results, @params);
    }

    public IEnumerable<SerializedObject> Primary(int cultureId, int objectId)
    {
      return this.dbSet.FromSql(
        this.GetUnsortedSelectQuerySql("ObjectId IN (SELECT PrimaryId FROM Relations WHERE ForeignId = {1})"),
        cultureId, objectId
      );
    }

    public IEnumerable<SerializedObject> Primary(int cultureId, int objectId, Params @params)
    {
      IQueryable<SerializedObject> results = this.dbSet.FromSql(
        this.GetSortedSelectQuerySql("SerializedObjects.ObjectId IN (SELECT PrimaryId FROM Relations WHERE ForeignId = {1})", @params),
        cultureId, objectId
      );

      return this.ApplyPaging(results, @params);
    }

    public IEnumerable<SerializedObject> Primary(int cultureId, int memberId, int objectId)
    {
      return this.dbSet.FromSql(
        this.GetUnsortedSelectQuerySql("ObjectId IN (SELECT PrimaryId FROM Relations WHERE MemberId = {1} AND ForeignId = {2})"),
        cultureId, memberId, objectId
      );
    }

    public IEnumerable<SerializedObject> Primary(int cultureId, int memberId, int objectId, Params @params)
    {
      IQueryable<SerializedObject> results = this.dbSet.FromSql(
        this.GetSortedSelectQuerySql("SerializedObjects.ObjectId IN (SELECT PrimaryId FROM Relations WHERE MemberId = {1} AND ForeignId = {2})", @params),
        cultureId, memberId, objectId
      );

      return this.ApplyPaging(results, @params);
    }

    public IEnumerable<SerializedObject> Foreign(int cultureId, int objectId)
    {
      return this.dbSet.FromSql(
        this.GetUnsortedSelectQuerySql("ObjectId IN (SELECT ForeignId FROM Relations WHERE PrimaryId = {1})"),
        cultureId, objectId
      );
    }

    public IEnumerable<SerializedObject> Foreign(int cultureId, int objectId, Params @params)
    {
      IQueryable<SerializedObject> results = this.dbSet.FromSql(
        this.GetSortedSelectQuerySql("SerializedObjects.ObjectId IN (SELECT ForeignId FROM Relations WHERE PrimaryId = {1})", @params),
        cultureId, objectId
      );

      return this.ApplyPaging(results, @params);
    }

    public IEnumerable<SerializedObject> Foreign(int cultureId, int memberId, int objectId)
    {
      return this.dbSet.FromSql(
        this.GetUnsortedSelectQuerySql("ObjectId IN (SELECT ForeignId FROM Relations WHERE MemberId = {1} AND PrimaryId = {2})"),
        cultureId, memberId, objectId
      );
    }

    public IEnumerable<SerializedObject> Foreign(int cultureId, int memberId, int objectId, Params @params)
    {
      IQueryable<SerializedObject> results = this.dbSet.FromSql(
        this.GetSortedSelectQuerySql("SerializedObjects.ObjectId IN (SELECT ForeignId FROM Relations WHERE MemberId = {1} AND PrimaryId = {2})", @params),
        cultureId, memberId, objectId
      );

      return this.ApplyPaging(results, @params);
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

    private string GetUnsortedSelectQuerySql(string additionalWhereClause)
    {
      StringBuilder sql = new StringBuilder("SELECT * FROM SerializedObjects WHERE CultureId = {0} AND ");

      sql.Append(additionalWhereClause);
      return sql.ToString();
    }

    private string GetSortedSelectQuerySql(string additionalWhereClause, Params @params)
    {
      if (@params.Sorting == null)
        return this.GetUnsortedSelectQuerySql(additionalWhereClause);

      StringBuilder sql = new StringBuilder(
        this.GetSortedBaseSelectQuerySql(additionalWhereClause, @params.Sorting.StorageDataType == StorageDataType.String)
      );

      if (@params.Sorting.StorageDataType == StorageDataType.Integer)
        sql.Append(this.GetSortedByIntegerValueSelectQuerySql(@params));

      if (@params.Sorting.StorageDataType == StorageDataType.Decimal)
        sql.Append(this.GetSortedByDecimalValueSelectQuerySql(@params));

      if (@params.Sorting.StorageDataType == StorageDataType.String)
        sql.Append(this.GetSortedByStringValueSelectQuerySql(@params));

      if (@params.Sorting.StorageDataType == StorageDataType.DateTime)
        sql.Append(this.GetSortedByDateTimeValueSelectQuerySql(@params));

      return sql.ToString();
    }

    private string GetSortedBaseSelectQuerySql(string additionalWhereClause, bool joinLocalizations)
    {
      return
        "SELECT SerializedObjects.CultureId, SerializedObjects.ObjectId, SerializedObjects.ClassId, SerializedObjects.UrlPropertyStringValue, SerializedObjects.SerializedProperties FROM SerializedObjects " +
        "INNER JOIN Classes ON Classes.Id = SerializedObjects.ClassId " +
        "INNER JOIN Members ON Members.ClassId = SerializedObjects.ClassId OR Members.ClassId = Classes.ClassId " +
        "INNER JOIN Properties ON Properties.ObjectId = SerializedObjects.ObjectId AND Properties.MemberId = Members.Id " +
        (joinLocalizations ? "INNER JOIN Localizations ON Localizations.DictionaryId = Properties.StringValueId " : null) +
        "WHERE SerializedObjects.CultureId = {0} AND " + additionalWhereClause + " AND ";
    }

    private string GetSortedByIntegerValueSelectQuerySql(Params @params)
    {
      return "Members.Id = " + @params.Sorting.MemberId + " ORDER BY Properties.IntegerValue " + @params.Sorting.Direction;
    }

    private string GetSortedByDecimalValueSelectQuerySql(Params @params)
    {
      return "Members.Id = " + @params.Sorting.MemberId + " ORDER BY Properties.DecimalValue " + @params.Sorting.Direction;
    }

    private string GetSortedByStringValueSelectQuerySql(Params @params)
    {
      string filteringSql = null;

      if (@params.Filtering != null)
        filteringSql = " AND Localizations.Value LIKE '%" + @params.Filtering.Query + "%'";

      return "Members.Id = " + @params.Sorting.MemberId + " AND (Localizations.CultureId = 1 OR Localizations.CultureId = {0})" + filteringSql + " ORDER BY Localizations.Value " + @params.Sorting.Direction;
    }

    private string GetSortedByDateTimeValueSelectQuerySql(Params @params)
    {
      return "Members.Id = " + @params.Sorting.MemberId + " ORDER BY datetime(Properties.DateTimeValue) " + @params.Sorting.Direction;
    }

    private IEnumerable<SerializedObject> ApplyPaging(IQueryable<SerializedObject> results, Params @params)
    {
      if (@params.Paging == null)
        return results;

      if (@params.Paging.Skip != null)
        results = results.Skip((int)@params.Paging.Skip);

      if (@params.Paging.Take != null)
        results = results.Take((int)@params.Paging.Take);

      return results;
    }
  }
}