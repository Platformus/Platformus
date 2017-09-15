// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="DataType"/> entities.
  /// </summary>
  public interface IDataTypeRepository : IRepository
  {
    DataType WithKey(int id);
    IEnumerable<DataType> All();
    IEnumerable<DataType> Range(string orderBy, string direction, int skip, int take, string filter);
    void Create(DataType dataType);
    void Edit(DataType dataType);
    void Delete(int id);
    void Delete(DataType dataType);
    int Count(string filter);
  }
}