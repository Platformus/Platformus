// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Configuration.Data.Models;

namespace Platformus.Configuration.Data.Abstractions
{
  public interface IVariableRepository : IRepository
  {
    Variable WithKey(int id);
    Variable WithSectionIdAndCode(int sectionId, string code);
    IEnumerable<Variable> FilteredBySectionId(int sectionId);
    void Create(Variable variable);
    void Edit(Variable variable);
    void Delete(int id);
    void Delete(Variable variable);
  }
}