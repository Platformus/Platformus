// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Role"/> entities.
  /// </summary>
  public interface IRoleRepository : IRepository
  {
    Role WithKey(int id);
    IEnumerable<Role> All();
    IEnumerable<Role> Range(string orderBy, string direction, int skip, int take, string filter);
    void Create(Role role);
    void Edit(Role role);
    void Delete(int id);
    void Delete(Role role);
    int Count(string filter);
  }
}