// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.Data.Abstractions
{
  public interface IFieldOptionRepository : IRepository
  {
    FieldOption WithKey(int id);
    IEnumerable<FieldOption> FilteredByFieldId(int fieldId);
    void Create(FieldOption fieldOption);
    void Edit(FieldOption fieldOption);
    void Delete(int id);
    void Delete(FieldOption fieldOption);
  }
}