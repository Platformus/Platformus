// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.EntityFramework.PostgreSql
{
  /// <summary>
  /// Implements the <see cref="IMemberRepository"/> interface and represents the repository
  /// for manipulating the <see cref="Member"/> entities in the context of PostgreSQL database.
  /// </summary>
  public class MemberRepository : RepositoryBase<Member>, IMemberRepository
  {
    /// <summary>
    /// Gets the member by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the member.</param>
    /// <returns>Found member with the given identifier.</returns>
    public Member WithKey(int id)
    {
      return this.dbSet.Find(id);
    }

    /// <summary>
    /// Gets the member by the class identifier and code (case insensitive).
    /// </summary>
    /// <param name="classId">The unique identifier of the class this member belongs to.</param>
    /// <param name="code">The unique code of the member.</param>
    /// <returns>Found member with the given class identifier and code.</returns>
    public Member WithClassIdAndCode(int classId, string code)
    {
      return this.dbSet.FirstOrDefault(m => m.ClassId == classId && string.Equals(m.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets the member by the class identifier (including members of the parent class) and code (case insensitive).
    /// </summary>
    /// <param name="classId">The unique identifier of the class this member belongs to.</param>
    /// <param name="code">The unique code of the member.</param>
    /// <returns>Found member with the given class identifier and code.</returns>
    public Member WithClassIdAndCodeInlcudingParent(int classId, string code)
    {
      return this.dbSet.AsNoTracking().FromSql(
        "SELECT * FROM \"Members\" WHERE \"ClassId\" = {0} OR \"ClassId\" IN (SELECT \"ClassId\" FROM \"Classes\" WHERE \"Id\" = {0})",
        classId
      ).FirstOrDefault(m => string.Equals(m.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Gets all the members using sorting by class identifier (ascending) and then by position (ascending).
    /// </summary>
    /// <returns>Found members.</returns>
    public IEnumerable<Member> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(m => m.ClassId).ThenBy(m => m.Position);
    }

    /// <summary>
    /// Gets the members filtered by the class identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the class these members belongs to.</param>
    /// <returns>Found members.</returns>
    public IEnumerable<Member> FilteredByClassId(int classId)
    {
      return this.dbSet.AsNoTracking().Where(m => m.ClassId == classId).OrderBy(m => m.Position);
    }

    /// <summary>
    /// Gets the members filtered by the class identifier (including members of the parent class) using sorting by position (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the class these members belongs to.</param>
    /// <returns>Found members.</returns>
    public IEnumerable<Member> FilteredByClassIdInlcudingParent(int classId)
    {
      return this.dbSet.AsNoTracking().FromSql(
        "SELECT * FROM \"Members\" WHERE \"ClassId\" = {0} OR \"ClassId\" IN (SELECT \"ClassId\" FROM \"Classes\" WHERE \"Id\" = {0}) ORDER BY \"Position\"",
        classId
      );
    }

    /// <summary>
    /// Gets the members (visible in list properties only) filtered by the class identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the class these members belongs to.</param>
    /// <returns>Found members.</returns>
    public IEnumerable<Member> FilteredByClassIdPropertyVisibleInList(int classId)
    {
      return this.dbSet.AsNoTracking().Where(m => m.ClassId == classId && m.IsPropertyVisibleInList == true).OrderBy(m => m.Position);
    }

    /// <summary>
    /// Gets the members (visible in list properties only) filtered by the class identifier (including members of the parent class)
    /// using sorting by position (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the class these members belongs to.</param>
    /// <returns>Found members.</returns>
    public IEnumerable<Member> FilteredByClassIdInlcudingParentPropertyVisibleInList(int classId)
    {
      return this.dbSet.AsNoTracking().FromSql(
        "SELECT * FROM \"Members\" WHERE (\"ClassId\" = {0} OR \"ClassId\" IN (SELECT \"ClassId\" FROM \"Classes\" WHERE \"Id\" = {0})) AND \"IsPropertyVisibleInList\" = {1} ORDER BY \"Position\"",
        classId, true
      );
    }

    /// <summary>
    /// Gets the members (relation single parent only) filtered by the class identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the class these members belongs to.</param>
    /// <returns>Found members.</returns>
    public IEnumerable<Member> FilteredByClassIdRelationSingleParent(int classId)
    {
      return this.dbSet.AsNoTracking().Where(m => m.ClassId == classId && m.IsRelationSingleParent == true).OrderBy(m => m.Position);
    }

    /// <summary>
    /// Gets all the members filtered by the class identifier using the given filtering, sorting, and paging.
    /// </summary>
    /// <param name="classId">The unique identifier of the class these members belongs to.</param>
    /// <param name="orderBy">The member property name to sort by.</param>
    /// <param name="direction">The sorting direction.</param>
    /// <param name="skip">The number of members that should be skipped.</param>
    /// <param name="take">The number of members that should be taken.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>Found members filtered by the class identifier using the given filtering, sorting, and paging.</returns>
    public IEnumerable<Member> FilteredByClassIdRange(int classId, string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredMembers(dbSet.AsNoTracking(), classId, filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    /// <summary>
    /// Creates the member.
    /// </summary>
    /// <param name="member">The member to create.</param>
    public void Create(Member member)
    {
      this.dbSet.Add(member);
    }

    /// <summary>
    /// Edits the member.
    /// </summary>
    /// <param name="member">The member to edit.</param>
    public void Edit(Member member)
    {
      this.storageContext.Entry(member).State = EntityState.Modified;
    }

    /// <summary>
    /// Deletes the member specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the member to delete.</param>
    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    /// <summary>
    /// Deletes the member.
    /// </summary>
    /// <param name="member">The member to delete.</param>
    public void Delete(Member member)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM ""DataTypeParameterValues"" WHERE ""MemberId"" = {0};
          CREATE TEMP TABLE ""TempDictionaries"" (""Id"" INT PRIMARY KEY);
          INSERT INTO ""TempDictionaries"" SELECT ""StringValueId"" FROM ""Properties"" WHERE ""MemberId"" = {0} AND ""StringValueId"" IS NOT NULL;
          DELETE FROM ""Properties"" WHERE ""MemberId"" = {0};
          DELETE FROM ""Localizations"" WHERE ""DictionaryId"" IN (SELECT ""Id"" FROM ""TempDictionaries"");
          DELETE FROM ""Dictionaries"" WHERE ""Id"" IN (SELECT ""Id"" FROM ""TempDictionaries"");
          DELETE FROM ""Relations"" WHERE ""MemberId"" = {0};
        ",
        member.Id
      );

      this.dbSet.Remove(member);
    }

    /// <summary>
    /// Counts the number of the members filtered by the class identifier with the given filtering.
    /// </summary>
    /// <param name="classId">The unique identifier of the class these members belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of members found.</returns>
    public int CountByClassId(int classId, string filter)
    {
      return this.GetFilteredMembers(dbSet, classId, filter).Count();
    }

    private IQueryable<Member> GetFilteredMembers(IQueryable<Member> members, int classId, string filter)
    {
      members = members.Where(m => m.ClassId == classId);

      if (string.IsNullOrEmpty(filter))
        return members;

      return members.Where(m => m.Name.ToLower().Contains(filter.ToLower()));
    }
  }
}