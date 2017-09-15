// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Object"/> entities.
  /// </summary>
  public interface IObjectRepository : IRepository
  {
    Object WithKey(int id);
    IEnumerable<Object> All();
    IEnumerable<Object> FilteredByClassId(int classId);
    void Create(Object @object);
    void Edit(Object @object);
    void Delete(int id);
    void Delete(Object @object);
  }
}