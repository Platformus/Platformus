// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  public interface IMicrocontrollerRepository : IRepository
  {
    Microcontroller WithKey(int id);
    IEnumerable<Microcontroller> All();
    IEnumerable<Microcontroller> Range(string orderBy, string direction, int skip, int take, string filter);
    void Create(Microcontroller microcontroller);
    void Edit(Microcontroller microcontroller);
    void Delete(int id);
    void Delete(Microcontroller microcontroller);
    int Count(string filter);
  }
}