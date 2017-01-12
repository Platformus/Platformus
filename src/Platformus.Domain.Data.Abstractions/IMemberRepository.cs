// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.Data.Abstractions
{
  public interface IMemberRepository : IRepository
  {
    Member WithKey(int id);
    Member WithClassIdAndCode(int classId, string code);
    IEnumerable<Member> FilteredByClassId(int classId);
    IEnumerable<Member> FilteredByClassIdInlcudingParent(int classId);
    IEnumerable<Member> FilteredByClassIdPropertyVisibleInList(int classId);
    IEnumerable<Member> FilteredByClassIdInlcudingParentPropertyVisibleInList(int classId);
    IEnumerable<Member> FilteredByRelationClassIdRelationSingleParent(int relationClassId);
    IEnumerable<Member> FilteredByClassIdRange(int classId, string orderBy, string direction, int skip, int take, string filter);
    void Create(Member member);
    void Edit(Member member);
    void Delete(int id);
    void Delete(Member member);
    int CountByClassId(int classId, string filter);
  }
}