// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Content.Data.Models;

namespace Platformus.Content.Data.Abstractions
{
  public interface ITabRepository : IRepository
  {
    Tab WithKey(int id);
    IEnumerable<Tab> All();
    IEnumerable<Tab> FilteredByClassId(int classId);
    IEnumerable<Tab> Range(int classId, string orderBy, string direction, int skip, int take);
    void Create(Tab tab);
    void Edit(Tab tab);
    void Delete(int id);
    void Delete(Tab tab);
    int CountByClassId(int classId);
  }
}