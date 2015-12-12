// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Content.Data.Models;

namespace Platformus.Content.Data.Abstractions
{
  public interface IObjectRepository : IRepository
  {
    Object WithKey(int id);
    Object WithUrl(string url);
    IEnumerable<Object> All();
    IEnumerable<Object> FilteredByClassId(int classId);
    IEnumerable<Object> FilteredByClassIdRange(int classId, string orderBy, string direction, int skip, int take);
    IEnumerable<Object> Primary(int objectId);
    IEnumerable<Object> Primary(int memberId, int objectId);
    IEnumerable<Object> Foreign(int objectId);
    IEnumerable<Object> Foreign(int memberId, int objectId);
    IEnumerable<Object> Standalone();
    void Create(Object @object);
    void Edit(Object @object);
    void Delete(int id);
    void Delete(Object @object);
    int CountByClassId(int classId);
  }
}