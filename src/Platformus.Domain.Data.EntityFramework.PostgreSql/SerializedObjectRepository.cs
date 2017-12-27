// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.EntityFramework.PostgreSql
{
  /// <summary>
  /// Implements the <see cref="ISerializedObjectRepository"/> interface and represents the repository
  /// for manipulating the <see cref="SerializedObject"/> entities in the context of PostgreSQL database.
  /// </summary>
  public class SerializedObjectRepository : RepositoryBase<SerializedObject>, ISerializedObjectRepository
  {
    /// <summary>
    /// Gets the serialized object by the culture identifier and object identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized object belongs to.</param>
    /// <param name="objectId">The unique identifier of the object this serialized object belongs to.</param>
    /// <returns>Found serialized object with the given culture identifier and object identifier.</returns>
    public SerializedObject WithKey(int cultureId, int objectId)
    {
      return this.dbSet.Find(cultureId, objectId);
    }

    /// <summary>
    /// Gets the serialized object by the culture identifier and URL object property value (case insensitive).
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized object belongs to.</param>
    /// <param name="urlPropertyStringValue">The URL object property value.</param>
    /// <returns>Found serialized object with the given culture identifier and URL object property value.</returns>
    public SerializedObject WithCultureIdAndUrlPropertyStringValue(int cultureId, string urlPropertyStringValue)
    {
      return this.dbSet.FirstOrDefault(so => so.CultureId == cultureId && string.Equals(so.UrlPropertyStringValue, urlPropertyStringValue, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets the serialized objects filtered by the culture identifier and class identifier
    /// using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="classId">The unique identifier of the class these serialized objects belongs to.</param>
    /// <param name="@params">The filtering, sorting, and paging parameters.</param>
    /// <returns>Found serialized objects.</returns>
    public IEnumerable<SerializedObject> FilteredByCultureIdAndClassId(int cultureId, int classId, Params @params)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetSortedSelectQuerySql("\"SerializedObjects\".\"ClassId\" = {1}", @params),
        cultureId, classId
      );
    }

    /// <summary>
    /// Gets the serialized objects filtered by the culture identifier, class identifier, and object identifier
    /// using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="classId">The unique identifier of the class these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <param name="@params">The filtering, sorting, and paging parameters.</param>
    /// <returns>Found serialized objects.</returns>
    public IEnumerable<SerializedObject> FilteredByCultureIdAndClassIdAndObjectId(int cultureId, int classId, int objectId, Params @params)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetSortedSelectQuerySql("\"SerializedObjects\".\"ClassId\" = {1} AND \"SerializedObjects\".\"ObjectId\" IN (SELECT \"PrimaryId\" FROM \"Relations\" WHERE \"ForeignId\" = {2})", @params),
        cultureId, classId, objectId
      );
    }

    /// <summary>
    /// Gets the primary serialized objects filtered by the culture identifier and object identifier
    /// using sorting by identifier (ascending).
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <returns>Found primary serialized objects.</returns>
    public IEnumerable<SerializedObject> Primary(int cultureId, int objectId)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetUnsortedSelectQuerySql("\"ObjectId\" IN (SELECT \"PrimaryId\" FROM \"Relations\" WHERE \"ForeignId\" = {1})"),
        cultureId, objectId
      );
    }

    /// <summary>
    /// Gets the primary serialized objects filtered by the culture identifier and object identifier
    /// using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <param name="@params">The filtering, sorting, and paging parameters.</param>
    /// <returns>Found primary serialized objects.</returns>
    public IEnumerable<SerializedObject> Primary(int cultureId, int objectId, Params @params)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetSortedSelectQuerySql("\"SerializedObjects\".\"ObjectId\" IN (SELECT \"PrimaryId\" FROM \"Relations\" WHERE \"ForeignId\" = {1})", @params),
        cultureId, objectId
      );
    }

    /// <summary>
    /// Gets the primary serialized objects filtered by the culture identifier, member identifier, and object identifier
    /// using sorting by identifier (ascending).
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="memberId">The unique identifier of the member these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <returns>Found primary serialized objects.</returns>
    public IEnumerable<SerializedObject> Primary(int cultureId, int memberId, int objectId)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetUnsortedSelectQuerySql("\"ObjectId\" IN (SELECT \"PrimaryId\" FROM \"Relations\" WHERE \"MemberId\" = {1} AND \"ForeignId\" = {2})"),
        cultureId, memberId, objectId
      );
    }

    /// <summary>
    /// Gets the primary serialized objects filtered by the culture identifier, member identifier, and object identifier
    /// using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="memberId">The unique identifier of the member these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <param name="@params">The filtering, sorting, and paging parameters.</param>
    /// <returns>Found primary serialized objects.</returns>
    public IEnumerable<SerializedObject> Primary(int cultureId, int memberId, int objectId, Params @params)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetSortedSelectQuerySql("\"SerializedObjects\".\"ObjectId\" IN (SELECT \"PrimaryId\" FROM \"Relations\" WHERE \"MemberId\" = {1} AND \"ForeignId\" = {2})", @params),
        cultureId, memberId, objectId
      );
    }

    /// <summary>
    /// Gets the foreign serialized objects filtered by the culture identifier and object identifier
    /// using sorting by identifier (ascending).
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <returns>Found foreign serialized objects.</returns>
    public IEnumerable<SerializedObject> Foreign(int cultureId, int objectId)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetUnsortedSelectQuerySql("\"ObjectId\" IN (SELECT \"PrimaryId\" FROM \"Relations\" WHERE \"ForeignId\" = {1})"),
        cultureId, objectId
      );
    }

    /// <summary>
    /// Gets the foreign serialized objects filtered by the culture identifier and object identifier
    /// using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <param name="@params">The filtering, sorting, and paging parameters.</param>
    /// <returns>Found foreign serialized objects.</returns>
    public IEnumerable<SerializedObject> Foreign(int cultureId, int objectId, Params @params)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetSortedSelectQuerySql("\"SerializedObjects\".\"ObjectId\" IN (SELECT \"PrimaryId\" FROM \"Relations\" WHERE \"ForeignId\" = {1})", @params),
        cultureId, objectId
      );
    }

    /// <summary>
    /// Gets the foreign serialized objects filtered by the culture identifier, member identifier, and object identifier
    /// using sorting by identifier (ascending).
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="memberId">The unique identifier of the member these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <returns>Found foreign serialized objects.</returns>
    public IEnumerable<SerializedObject> Foreign(int cultureId, int memberId, int objectId)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetUnsortedSelectQuerySql("\"ObjectId\" IN (SELECT \"ForeignId\" FROM \"Relations\" WHERE \"MemberId\" = {1} AND \"PrimaryId\" = {2})"),
        cultureId, memberId, objectId
      );
    }

    /// <summary>
    /// Gets the foreign serialized objects filtered by the culture identifier, member identifier, and object identifier
    /// using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="memberId">The unique identifier of the member these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <param name="@params">The filtering, sorting, and paging parameters.</param>
    /// <returns>Found foreign serialized objects.</returns>
    public IEnumerable<SerializedObject> Foreign(int cultureId, int memberId, int objectId, Params @params)
    {
      return this.dbSet.AsNoTracking().FromSql(
        this.GetSortedSelectQuerySql("\"SerializedObjects\".\"ObjectId\" IN (SELECT \"ForeignId\" FROM \"Relations\" WHERE \"MemberId\" = {1} AND \"PrimaryId\" = {2})", @params),
        cultureId, memberId, objectId
      );
    }

    /// <summary>
    /// Creates the serialized object.
    /// </summary>
    /// <param name="serializedObject">The serialized object to create.</param>
    public void Create(SerializedObject serializedObject)
    {
      this.dbSet.Add(serializedObject);
    }

    /// <summary>
    /// Edits the serialized object.
    /// </summary>
    /// <param name="serializedObject">The serialized object to edit.</param>
    public void Edit(SerializedObject serializedObject)
    {
      this.storageContext.Entry(serializedObject).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the serialized object specified by the culture identifier and object identifier.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture this serialized object belongs to.</param>
    /// <param name="objectId">The unique identifier of the object this serialized object belongs to.</param>
    public void Delete(int cultureId, int objectId)
    {
      this.Delete(this.WithKey(cultureId, objectId));
    }

    /// <summary>
    /// Deletes the serialized object.
    /// </summary>
    /// <param name="serializedObject">The serialized object to delete.</param>
    public void Delete(SerializedObject serializedObject)
    {
      this.dbSet.Remove(serializedObject);
    }

    /// <summary>
    /// Counts the number of the serialized objects filtered by the culture identifier and class identifier with the given filtering.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="classId">The unique identifier of the class these serialized objects belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of serialized objects found.</returns>
    public int CountByCultureIdAndClassId(int cultureId, int classId, Params @params)
    {
      if (@params == null || @params.Filtering == null || string.IsNullOrEmpty(@params.Filtering.Query))
        return this.dbSet.Count(so => so.CultureId == cultureId && so.ClassId == classId);

      return this.CountByRawSql(
        "SELECT COUNT(*) FROM \"SerializedObjects\" WHERE \"CultureId\" = @CultureId AND \"ClassId\" = @ClassId AND \"ObjectId\" IN (SELECT \"ObjectId\" FROM \"Properties\" WHERE \"StringValueId\" IN (SELECT \"DictionaryId\" FROM \"Localizations\" WHERE \"Value\" LIKE @Query))",
        new KeyValuePair<string, object>("@CultureId", cultureId),
        new KeyValuePair<string, object>("@ClassId", classId),
        new KeyValuePair<string, object>("@Query", "%" + @params.Filtering.Query + "%")
      );
    }

    /// <summary>
    /// Counts the number of the serialized objects filtered by the culture identifier, class identifier, and object identifier
    /// with the given filtering.
    /// </summary>
    /// <param name="cultureId">The unique identifier of the culture these serialized objects belongs to.</param>
    /// <param name="classId">The unique identifier of the class these serialized objects belongs to.</param>
    /// <param name="objectId">The unique identifier of the object these serialized objects belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of serialized objects found.</returns>
    public int CountByCultureIdAndClassIdAndObjectId(int cultureId, int classId, int objectId, Params @params)
    {
      if (@params == null || @params.Filtering == null || string.IsNullOrEmpty(@params.Filtering.Query))
        return this.CountByRawSql(
          "SELECT COUNT(*) FROM \"SerializedObjects\" WHERE \"CultureId\" = @CultureId AND \"ClassId\" = @ClassId AND \"ObjectId\" IN (SELECT \"PrimaryId\" FROM \"Relations\" WHERE \"ForeignId\" = @ObjectId)",
          new KeyValuePair<string, object>("@CultureId", cultureId),
          new KeyValuePair<string, object>("@ClassId", classId),
          new KeyValuePair<string, object>("@ObjectId", objectId)
        );

      return this.CountByRawSql(
        "SELECT COUNT(*) FROM \"SerializedObjects\" WHERE \"CultureId\" = @CultureId AND \"ClassId\" = @ClassId AND \"ObjectId\" IN (SELECT \"PrimaryId\" FROM \"Relations\" WHERE \"ForeignId\" = @ObjectId) AND \"ObjectId\" IN (SELECT \"ObjectId\" FROM \"Properties\" WHERE \"StringValueId\" IN (SELECT \"DictionaryId\" FROM \"Localizations\" WHERE \"Value\" LIKE @Query))",
        new KeyValuePair<string, object>("@CultureId", cultureId),
        new KeyValuePair<string, object>("@ClassId", classId),
        new KeyValuePair<string, object>("@ObjectId", objectId),
        new KeyValuePair<string, object>("@Query", "%" + @params.Filtering.Query + "%")
      );
    }

    private string GetUnsortedSelectQuerySql(string additionalWhereClause)
    {
      StringBuilder sql = new StringBuilder("SELECT * FROM \"SerializedObjects\" WHERE \"CultureId\" = {0} AND ");

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
        sql.Append($" LIMIT {@params.Paging.Take} OFFSET {@params.Paging.Skip}");

      return sql.ToString();
    }

    private string GetSortedBaseSelectQuerySql(string additionalWhereClause, bool joinLocalizations)
    {
      return
        "SELECT \"SerializedObjects\".\"CultureId\", \"SerializedObjects\".\"ObjectId\", \"SerializedObjects\".\"ClassId\", \"SerializedObjects\".\"UrlPropertyStringValue\", \"SerializedObjects\".\"SerializedProperties\" FROM \"SerializedObjects\" " +
        "INNER JOIN \"Classes\" ON \"Classes\".\"Id\" = \"SerializedObjects\".\"ClassId\" " +
        "INNER JOIN \"Members\" ON \"Members\".\"ClassId\" = \"SerializedObjects\".\"ClassId\" OR \"Members\".\"ClassId\" = \"Classes\".\"ClassId\" " +
        "INNER JOIN \"Properties\" ON \"Properties\".\"ObjectId\" = \"SerializedObjects\".\"ObjectId\" AND \"Properties\".\"MemberId\" = \"Members\".\"Id\" " +
        (joinLocalizations ? "INNER JOIN \"Localizations\" ON \"Localizations\".\"DictionaryId\" = \"Properties\".\"StringValueId\" " : null) +
        "WHERE \"SerializedObjects\".\"CultureId\" = {0} AND " + additionalWhereClause + " AND ";
    }

    private string GetSortedByIntegerValueSelectQuerySql(Params @params)
    {
      return "\"Members\".\"Id\" = " + @params.Sorting.MemberId + " ORDER BY \"Properties\".\"IntegerValue\" " + @params.Sorting.Direction;
    }

    private string GetSortedByDecimalValueSelectQuerySql(Params @params)
    {
      return "\"Members\".\"Id\" = " + @params.Sorting.MemberId + " ORDER BY \"Properties\".\"DecimalValue\" " + @params.Sorting.Direction;
    }

    private string GetSortedByStringValueSelectQuerySql(Params @params)
    {
      string filteringSql = null;

      if (@params.Filtering != null)
        filteringSql = " AND \"SerializedObjects\".\"ObjectId\" IN (SELECT \"ObjectId\" FROM \"Properties\" WHERE \"StringValueId\" IN (SELECT \"DictionaryId\" FROM \"Localizations\" WHERE \"Value\" LIKE '%" + @params.Filtering.Query + "%'))";

      return "\"Members\".\"Id\" = " + @params.Sorting.MemberId + " AND (\"Localizations\".\"CultureId\" = 1 OR \"Localizations\".\"CultureId\" = {0})" + filteringSql + " ORDER BY \"Localizations\".\"Value\" " + @params.Sorting.Direction;
    }

    private string GetSortedByDateTimeValueSelectQuerySql(Params @params)
    {
      return "\"Members\".\"Id\" = " + @params.Sorting.MemberId + " ORDER BY \"Properties\".\"DateTimeValue\" " + @params.Sorting.Direction;
    }

    public int CountByRawSql(string sql, params KeyValuePair<string, object>[] parameters)
    {
      int result = 0;
      NpgsqlConnection connection = (this.storageContext as DbContext).Database.GetDbConnection() as NpgsqlConnection;

      try
      {
        connection.Open();

        using (NpgsqlCommand command = connection.CreateCommand())
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