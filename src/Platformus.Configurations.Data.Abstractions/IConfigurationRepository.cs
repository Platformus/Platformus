// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Configurations.Data.Entities;

namespace Platformus.Configurations.Data.Abstractions
{
  public interface IConfigurationRepository : IRepository
  {
    Configuration WithKey(int id);
    Configuration WithCode(string code);
    IEnumerable<Configuration> All();
    void Create(Configuration configuration);
    void Edit(Configuration configuration);
    void Delete(int id);
    void Delete(Configuration configuration);
  }
}