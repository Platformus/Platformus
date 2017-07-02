// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  public interface ITabRepository : IRepository
  {
    Tab WithKey(int id);
    IEnumerable<Tab> FilteredByClassId(int classId);
    IEnumerable<Tab> FilteredByClassIdInlcudingParent(int classId);
    IEnumerable<Tab> FilteredByClassIdRange(int classId, string orderBy, string direction, int skip, int take, string filter);
    void Create(Tab tab);
    void Edit(Tab tab);
    void Delete(int id);
    void Delete(Tab tab);
    int CountByClassId(int classId, string filter);
  }
}