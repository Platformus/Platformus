// Copyright © 2017 Dmitry Yegorov. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="DataTypeParameter"/> entities.
  /// </summary>
  public interface IDataTypeParameterRepository : IRepository
  {
    DataTypeParameter WithKey(int id);
    IEnumerable<DataTypeParameter> All();
    IEnumerable<DataTypeParameter> FilteredByDataTypeId(int dataTypeId);
    IEnumerable<DataTypeParameter> FilteredByDataTypeIdRange(int dataTypeId, string orderBy, string direction, int skip, int take, string filter);
    void Create(DataTypeParameter dataTypeParameter);
    void Edit(DataTypeParameter dataTypeParameter);
    void Delete(int id);
    void Delete(DataTypeParameter dataTypeParameter);
    int CountByDataTypeId(int dataTypeId, string filter);
  }
}