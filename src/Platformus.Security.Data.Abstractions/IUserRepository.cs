// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Security.Data.Models;

namespace Platformus.Security.Data.Abstractions
{
  public interface IUserRepository : IRepository
  {
    User WithKey(int id);
    IEnumerable<User> Range(string orderBy, string direction, int skip, int take);
    void Create(User user);
    void Edit(User user);
    void Delete(int id);
    void Delete(User user);
    int Count();
  }
}