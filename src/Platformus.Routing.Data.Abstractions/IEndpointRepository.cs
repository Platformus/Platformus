// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Routing.Data.Entities;

namespace Platformus.Routing.Data.Abstractions
{
  /// <summary>
  /// Describes a repository for manipulating the <see cref="Endpoint"/> entities.
  /// </summary>
  public interface IEndpointRepository : IRepository
  {
    Endpoint WithKey(int id);
    IEnumerable<Endpoint> All();
    IEnumerable<Endpoint> Range(string orderBy, string direction, int skip, int take, string filter);
    void Create(Endpoint endpoint);
    void Edit(Endpoint endpoint);
    void Delete(int id);
    void Delete(Endpoint endpoint);
    int Count(string filter);
  }
}