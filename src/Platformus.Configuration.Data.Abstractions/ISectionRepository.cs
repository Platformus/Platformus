// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Configuration.Data.Models;

namespace Platformus.Configuration.Data.Abstractions
{
  public interface ISectionRepository : IRepository
  {
    Section WithKey(int id);
    Section WithCode(string code);
    IEnumerable<Section> All();
    void Create(Section section);
    void Edit(Section section);
    void Delete(int id);
    void Delete(Section section);
  }
}