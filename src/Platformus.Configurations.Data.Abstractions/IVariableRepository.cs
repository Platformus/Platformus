// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Configurations.Data.Entities;

namespace Platformus.Configurations.Data.Abstractions
{
  public interface IVariableRepository : IRepository
  {
    Variable WithKey(int id);
    Variable WithConfigurationIdAndCode(int configurationId, string code);
    IEnumerable<Variable> FilteredByConfigurationId(int configurationId);
    void Create(Variable variable);
    void Edit(Variable variable);
    void Delete(int id);
    void Delete(Variable variable);
  }
}