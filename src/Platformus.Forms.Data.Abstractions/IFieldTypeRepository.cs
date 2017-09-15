// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="FieldType"/> entities.
  /// </summary>
  public interface IFieldTypeRepository : IRepository
  {
    FieldType WithKey(int id);
    IEnumerable<FieldType> All();
  }
}