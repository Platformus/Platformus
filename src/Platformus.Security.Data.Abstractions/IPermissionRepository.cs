// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Data.Abstractions
{
  public interface IPermissionRepository : IRepository
  {
    Permission WithKey(int id);
    IEnumerable<Permission> All();
    IEnumerable<Permission> Range(string orderBy, string direction, int skip, int take, string filter);
    void Create(Permission permission);
    void Edit(Permission permission);
    void Delete(int id);
    void Delete(Permission permission);
    int Count(string filter);
  }
}