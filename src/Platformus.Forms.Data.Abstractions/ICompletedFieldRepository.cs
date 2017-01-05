// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Forms.Data.Models;

namespace Platformus.Forms.Data.Abstractions
{
  public interface ICompletedFieldRepository : IRepository
  {
    IEnumerable<CompletedField> FilteredByCompletedFormId(int completedFormId);
    void Create(CompletedField completedField);
  }
}