// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.Data.Abstractions
{
  public interface IObjectRepository : IRepository
  {
    Object WithKey(int id);
    Object WithUrl(string url);
    IEnumerable<Object> All();
    IEnumerable<Object> FilteredByClassId(int classId);

    // TODO: must be changed!
    IEnumerable<Object> FilteredByClassId(int classId, string storageDataType, int orderByMemberId, string direction, int cultureId);
    IEnumerable<Object> FilteredByClassIdRange(int classId, string orderBy, string direction, int skip, int take);
    IEnumerable<Object> FilteredByClassIdAndObjectIdRange(int classId, int objectId, string orderBy, string direction, int skip, int take);
    IEnumerable<Object> Primary(int objectId);

    // TODO: must be changed!
    IEnumerable<Object> Primary(int objectId, string storageDataType, int orderByMemberIdy, string direction, int cultureId);
    IEnumerable<Object> Primary(int memberId, int objectId);

    // TODO: must be changed!
    IEnumerable<Object> Primary(int memberId, int objectId, string storageDataType, int orderByMemberId, string direction, int cultureId);
    IEnumerable<Object> Foreign(int objectId);

    // TODO: must be changed!
    IEnumerable<Object> Foreign(int objectId, string storageDataType, int orderByMemberId, string direction, int cultureId);
    IEnumerable<Object> Foreign(int memberId, int objectId);

    // TODO: must be changed!
    IEnumerable<Object> Foreign(int memberId, int objectId, string storageDataType, int orderByMemberId, string direction, int cultureId);
    void Create(Object @object);
    void Edit(Object @object);
    void Delete(int id);
    void Delete(Object @object);
    int CountByClassId(int classId);
  }
}