// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.Data.Abstractions
{
  public interface IDataSourceRepository : IRepository
  {
    DataSource WithKey(int id);
    IEnumerable<DataSource> FilteredByClassId(int classId);
    IEnumerable<DataSource> FilteredByClassIdInlcudingParent(int classId);
    IEnumerable<DataSource> FilteredByClassIdRange(int classId, string orderBy, string direction, int skip, int take);
    void Create(DataSource dataSource);
    void Edit(DataSource dataSource);
    void Delete(int id);
    void Delete(DataSource dataSource);
    int CountByClassId(int classId);
  }
}