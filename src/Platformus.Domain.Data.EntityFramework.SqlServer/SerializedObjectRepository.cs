// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="ISerializedObjectRepository"/> interface and represents the repository
  /// for manipulating the <see cref="SerializedObject"/> entities in the context of SQL Server database.
  /// </summary>
  public class SerializedObjectRepository : RepositoryBase<SerializedObject>, ISerializedObjectRepository
  {
    public SerializedObject WithKey(int cultureId, int objectId)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(so => so.CultureId == cultureId && so.ObjectId == objectId);
    }

    public SerializedObject WithCultureIdAndUrlPropertyStringValue(int cultureId, string urlPropertyStringValue)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(so => so.CultureId == cultureId && string.Equals(so.UrlPropertyStringValue, urlPropertyStringValue, System.StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<SerializedObject> FilteredByCultureIdAndClassId(int cultureId, int classId)
    {
      return this.dbSet.AsNoTracking().Where(so => so.CultureId == cultureId && so.ClassId == classId);
    }

    public IEnumerable<SerializedObject> FilteredByCultureIdAndClassId(int cultureId, int classId, Params @params)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetSortedSelectQuerySql("SerializedObjects.ClassId = {1}", @params),
        cultureId, classId
      );
    }

    public IEnumerable<SerializedObject> FilteredByCultureIdAndClassIdAndObjectId(int cultureId, int classId, int objectId, Params @params)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetSortedSelectQuerySql("SerializedObjects.ClassId = {1} AND SerializedObjects.ObjectId IN (SELECT PrimaryId FROM Relations WHERE ForeignId = {2})", @params),
        cultureId, classId, objectId
      );
    }

    public IEnumerable<SerializedObject> Primary(int cultureId, int objectId)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetUnsortedSelectQuerySql("ObjectId IN (SELECT PrimaryId FROM Relations WHERE ForeignId = {1})"),
        cultureId, objectId
      );
    }

    public IEnumerable<SerializedObject> Primary(int cultureId, int objectId, Params @params)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetSortedSelectQuerySql("SerializedObjects.ObjectId IN (SELECT PrimaryId FROM Relations WHERE ForeignId = {1})", @params),
        cultureId, objectId
      );
    }

    public IEnumerable<SerializedObject> Primary(int cultureId, int memberId, int objectId)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetUnsortedSelectQuerySql("ObjectId IN (SELECT PrimaryId FROM Relations WHERE MemberId = {1} AND ForeignId = {2})"),
        cultureId, memberId, objectId
      );
    }

    public IEnumerable<SerializedObject> Primary(int cultureId, int memberId, int objectId, Params @params)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetSortedSelectQuerySql("SerializedObjects.ObjectId IN (SELECT PrimaryId FROM Relations WHERE MemberId = {1} AND ForeignId = {2})", @params),
        cultureId, memberId, objectId
      );
    }

    public IEnumerable<SerializedObject> Foreign(int cultureId, int objectId)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetUnsortedSelectQuerySql("ObjectId IN (SELECT PrimaryId FROM Relations WHERE ForeignId = {1})"),
        cultureId, objectId
      );
    }

    public IEnumerable<SerializedObject> Foreign(int cultureId, int objectId, Params @params)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetSortedSelectQuerySql("SerializedObjects.ObjectId IN (SELECT PrimaryId FROM Relations WHERE ForeignId = {1})", @params),
        cultureId, objectId
      );
    }

    public IEnumerable<SerializedObject> Foreign(int cultureId, int memberId, int objectId)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetUnsortedSelectQuerySql("ObjectId IN (SELECT ForeignId FROM Relations WHERE MemberId = {1} AND PrimaryId = {2})"),
        cultureId, memberId, objectId
      );
    }

    public IEnumerable<SerializedObject> Foreign(int cultureId, int memberId, int objectId, Params @params)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetSortedSelectQuerySql("SerializedObjects.ObjectId IN (SELECT ForeignId FROM Relations WHERE MemberId = {1} AND PrimaryId = {2})", @params),
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

    public int CountByCultureIdAndClassId(int cultureId, int classId, Params @params)
    {
      if (@params == null || @params.Filtering == null || string.IsNullOrEmpty(@params.Filtering.Query))
        return this.dbSet.Count(so => so.CultureId == cultureId && so.ClassId == classId);

      return this.CountByRawSql(
        "SELECT COUNT(*) FROM SerializedObjects WHERE CultureId = @CultureId AND ClassId = @ClassId AND ObjectId IN (SELECT ObjectId FROM Properties WHERE StringValueId IN (SELECT DictionaryId FROM Localizations WHERE Value LIKE @Query))",
        new KeyValuePair<string, object>("@CultureId", cultureId),
        new KeyValuePair<string, object>("@ClassId", classId),
        new KeyValuePair<string, object>("@Query", "%" + @params.Filtering.Query + "%")
      );
    }

    public int CountByCultureIdAndClassIdAndObjectId(int cultureId, int classId, int objectId, Params @params)
    {
      if (@params == null || @params.Filtering == null || string.IsNullOrEmpty(@params.Filtering.Query))
        return this.CountByRawSql(
          "SELECT COUNT(*) FROM SerializedObjects WHERE CultureId = @CultureId AND ClassId = @ClassId AND ObjectId IN (SELECT PrimaryId FROM Relations WHERE ForeignId = @ObjectId)",
          new KeyValuePair<string, object>("@CultureId", cultureId),
          new KeyValuePair<string, object>("@ClassId", classId),
          new KeyValuePair<string, object>("@ObjectId", objectId)
        );

      return this.CountByRawSql(
        "SELECT COUNT(*) FROM SerializedObjects WHERE CultureId = @CultureId AND ClassId = @ClassId AND ObjectId IN (SELECT PrimaryId FROM Relations WHERE ForeignId = @ObjectId) AND ObjectId IN (SELECT ObjectId FROM Properties WHERE StringValueId IN (SELECT DictionaryId FROM Localizations WHERE Value LIKE @Query))",
        new KeyValuePair<string, object>("@CultureId", cultureId),
        new KeyValuePair<string, object>("@ClassId", classId),
        new KeyValuePair<string, object>("@ObjectId", objectId),
        new KeyValuePair<string, object>("@Query", "%" + @params.Filtering.Query + "%")
      );
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

      if (@params.Paging != null)
        sql.Append($" OFFSET {@params.Paging.Skip} ROWS FETCH NEXT {@params.Paging.Take} ROWS ONLY");

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
        filteringSql = " AND SerializedObjects.ObjectId IN (SELECT ObjectId FROM Properties WHERE StringValueId IN (SELECT DictionaryId FROM Localizations WHERE Value LIKE '%" + @params.Filtering.Query + "%'))";

      return "Members.Id = " + @params.Sorting.MemberId + " AND (Localizations.CultureId = 1 OR Localizations.CultureId = {0})" + filteringSql + " ORDER BY Localizations.Value " + @params.Sorting.Direction;
    }

    private string GetSortedByDateTimeValueSelectQuerySql(Params @params)
    {
      return "Members.Id = " + @params.Sorting.MemberId + " ORDER BY Properties.DateTimeValue " + @params.Sorting.Direction;
    }

    public int CountByRawSql(string sql, params KeyValuePair<string, object>[] parameters)
    {
      int result = 0;
      SqlConnection connection = (this.storageContext as DbContext).Database.GetDbConnection() as SqlConnection;

      try
      {
        connection.Open();

        using (SqlCommand command = connection.CreateCommand())
        {
          command.CommandText = sql;

          foreach (KeyValuePair<string, object> parameter in parameters)
            command.Parameters.AddWithValue(parameter.Key, parameter.Value);

          using (DbDataReader dataReader = command.ExecuteReader())
            if (dataReader.HasRows)
              while (dataReader.Read())
                result = dataReader.GetInt32(0);
        }
      }

      catch (System.Exception e) { }

      finally { connection.Close(); }

      return result;
    }
  }
}