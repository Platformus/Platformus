// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Content.Data.Models;

namespace Platformus.Content.Data.Abstractions
{
  public interface IDataTypeRepository : IRepository
  {
    DataType WithKey(int id);
    IEnumerable<DataType> All();
    IEnumerable<DataType> Range(string orderBy, string direction, int skip, int take);
    void Create(DataType dataType);
    void Edit(DataType dataType);
    void Delete(int id);
    void Delete(DataType dataType);
    int Count();
  }
}