// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  public interface IObjectRepository : IRepository
  {
    Object WithKey(int id);
    Object WithUrl(string url);
    IEnumerable<Object> All();
    IEnumerable<Object> FilteredByClassId(int classId);
    IEnumerable<Object> FilteredByClassIdRange(int classId, string orderBy, string direction, int skip, int take);
    IEnumerable<Object> FilteredByClassIdAndObjectIdRange(int classId, int objectId, string orderBy, string direction, int skip, int take);
    void Create(Object @object);
    void Edit(Object @object);
    void Delete(int id);
    void Delete(Object @object);
    int CountByClassId(int classId);
  }
}