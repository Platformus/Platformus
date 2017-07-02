// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Security.Data.Entities;

namespace Platformus.Security.Data.Abstractions
{
  public interface IUserRoleRepository : IRepository
  {
    UserRole WithKey(int userId, int roleId);
    IEnumerable<UserRole> FilteredByUserId(int userId);
    void Create(UserRole userRole);
    void Edit(UserRole userRole);
    void Delete(int userId, int roleId);
    void Delete(UserRole userRole);
  }
}