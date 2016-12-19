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
  public class MemberRepository : RepositoryBase<Member>, IMemberRepository
  {
    public Member WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(m => m.Id == id);
    }

    public Member WithClassIdAndCode(int classId, string code)
    {
      return this.dbSet.FirstOrDefault(m => m.ClassId == classId && m.Code == code);
    }

    public IEnumerable<Member> FilteredByClassId(int classId)
    {
      return this.dbSet.Where(m => m.ClassId == classId).OrderBy(m => m.Position);
    }

    public IEnumerable<Member> FilteredByClassIdInlcudingParent(int classId)
    {
      return this.dbSet.FromSql(
        "SELECT * FROM Members WHERE ClassId = {0} OR ClassId IN (SELECT ClassId FROM Classes WHERE Id = {0}) ORDER BY Position",
        classId
      );
    }

    public IEnumerable<Member> FilteredByClassIdPropertyVisibleInList(int classId)
    {
      // TODO: workaround for #5899
      //return this.dbSet.Where(m => m.ClassId == classId && m.IsPropertyVisibleInList == true).OrderBy(m => m.Position);
      return this.dbSet.Where(m => m.ClassId == classId && m.IsPropertyVisibleInList != null).OrderBy(m => m.Position);
    }

    public IEnumerable<Member> FilteredByClassIdInlcudingParentPropertyVisibleInList(int classId)
    {
      return this.dbSet.FromSql(
        "SELECT * FROM Members WHERE (ClassId = {0} OR ClassId IN (SELECT ClassId FROM Classes WHERE Id = {0})) AND IsPropertyVisibleInList = {1} ORDER BY Position",
        classId, true
      );
    }

    public IEnumerable<Member> FilteredByRelationClassIdRelationSingleParent(int relationClassId)
    {
      // TODO: workaround for #5899
      //return this.dbSet.Where(m => m.RelationClassId == relationClassId && m.IsRelationSingleParent == true).OrderBy(m => m.Position);
      return this.dbSet.Where(m => m.RelationClassId == relationClassId && m.IsRelationSingleParent != null).OrderBy(m => m.Position);
    }

    public IEnumerable<Member> FilteredByClassRange(int classId, string orderBy, string direction, int skip, int take)
    {
      return this.dbSet.Where(m => m.ClassId == classId).OrderBy(m => m.Position).Skip(skip).Take(take);
    }

    public void Create(Member member)
    {
      this.dbSet.Add(member);
    }

    public void Edit(Member member)
    {
      this.storageContext.Entry(member).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Member member)
    {
      this.storageContext.Database.ExecuteSqlCommand(
        @"
          DELETE FROM CachedObjects WHERE ClassId IN (SELECT ClassId FROM Members WHERE Id = {0});
          CREATE TABLE #Dictionaries (Id INT PRIMARY KEY);
          INSERT INTO #Dictionaries SELECT HtmlId FROM Properties WHERE MemberId = {0};
          DELETE FROM Properties WHERE MemberId = {0};
          DELETE FROM Localizations WHERE DictionaryId IN (SELECT Id FROM #Dictionaries);
          DELETE FROM Dictionaries WHERE Id IN (SELECT Id FROM #Dictionaries);
        ",
        member.Id
      );

      this.dbSet.Remove(member);
    }

    public int CountByClassId(int classId)
    {
      return this.dbSet.Count(m => m.ClassId == classId);
    }
  }
}