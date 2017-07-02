// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  public interface IDataSourceRepository : IRepository
  {
    DataSource WithKey(int id);
    IEnumerable<DataSource> FilteredByMicrocontrollerId(int microcontrollerId);
    IEnumerable<DataSource> FilteredByMicrocontrollerIdRange(int microcontrollerId, string orderBy, string direction, int skip, int take, string filter);
    void Create(DataSource dataSource);
    void Edit(DataSource dataSource);
    void Delete(int id);
    void Delete(DataSource dataSource);
    int CountByMicrocontrollerId(int microcontrollerId, string filter);
  }
}