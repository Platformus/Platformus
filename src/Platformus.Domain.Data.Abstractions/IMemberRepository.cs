// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Member"/> entities.
  /// </summary>
  public interface IMemberRepository : IRepository
  {
    /// <summary>
    /// Gets the member by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the member.</param>
    /// <returns>Found member with the given identifier.</returns>
    Member WithKey(int id);

    /// <summary>
    /// Gets the member by the class identifier and code (case insensitive).
    /// </summary>
    /// <param name="classId">The unique identifier of the class this member belongs to.</param>
    /// <param name="code">The unique code of the member.</param>
    /// <returns>Found member with the given class identifier and code.</returns>
    Member WithClassIdAndCode(int classId, string code);

    /// <summary>
    /// Gets the member by the class identifier (including members of the parent class) and code (case insensitive).
    /// </summary>
    /// <param name="classId">The unique identifier of the class this member belongs to.</param>
    /// <param name="code">The unique code of the member.</param>
    /// <returns>Found member with the given class identifier and code.</returns>
    Member WithClassIdAndCodeInlcudingParent(int classId, string code);

    /// <summary>
    /// Gets all the members using sorting by class identifier (ascending) and then by position (ascending).
    /// </summary>
    /// <returns>Found members.</returns>
    IEnumerable<Member> All();

    /// <summary>
    /// Gets the members filtered by the class identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the class these members belongs to.</param>
    /// <returns>Found members.</returns>
    IEnumerable<Member> FilteredByClassId(int classId);

    /// <summary>
    /// Gets the members filtered by the class identifier (including members of the parent class) using sorting by position (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the class these members belongs to.</param>
    /// <returns>Found members.</returns>
    IEnumerable<Member> FilteredByClassIdInlcudingParent(int classId);

    /// <summary>
    /// Gets the members (visible in list properties only) filtered by the class identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the class these members belongs to.</param>
    /// <returns>Found members.</returns>
    IEnumerable<Member> FilteredByClassIdPropertyVisibleInList(int classId);

    /// <summary>
    /// Gets the members (visible in list properties only) filtered by the class identifier (including members of the parent class)
    /// using sorting by position (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the class these members belongs to.</param>
    /// <returns>Found members.</returns>
    IEnumerable<Member> FilteredByClassIdInlcudingParentPropertyVisibleInList(int classId);

    /// <summary>
    /// Gets the members (relation single parent only) filtered by the class identifier using sorting by position (ascending).
    /// </summary>
    /// <param name="classId">The unique identifier of the class these members belongs to.</param>
    /// <returns>Found members.</returns>
    IEnumerable<Member> FilteredByClassIdRelationSingleParent(int classId);

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
    IEnumerable<Member> FilteredByClassIdRange(int classId, string orderBy, string direction, int skip, int take, string filter);

    /// <summary>
    /// Creates the member.
    /// </summary>
    /// <param name="member">The member to create.</param>
    void Create(Member member);

    /// <summary>
    /// Edits the member.
    /// </summary>
    /// <param name="member">The member to edit.</param>
    void Edit(Member member);

    /// <summary>
    /// Deletes the member specified by the identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the member to delete.</param>
    void Delete(int id);

    /// <summary>
    /// Deletes the member.
    /// </summary>
    /// <param name="member">The member to delete.</param>
    void Delete(Member member);

    /// <summary>
    /// Counts the number of the members filtered by the class identifier with the given filtering.
    /// </summary>
    /// <param name="classId">The unique identifier of the class these members belongs to.</param>
    /// <param name="filter">The filtering query.</param>
    /// <returns>The number of members found.</returns>
    int CountByClassId(int classId, string filter);
  }
}