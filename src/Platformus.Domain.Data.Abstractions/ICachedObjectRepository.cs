// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain.Data.Abstractions
{
  public interface ICachedObjectRepository : IRepository
  {
    CachedObject WithKey(int cultureId, int objectId);
    CachedObject WithCultureIdAndUrl(int cultureId, string url);
    IEnumerable<CachedObject> FilteredByCultureId(int cultureId);
    IEnumerable<CachedObject> Primary(int cultureId, int objectId);
    IEnumerable<CachedObject> Primary(int cultureId, int memberId, int objectId);
    IEnumerable<CachedObject> Foreign(int cultureId, int objectId);
    IEnumerable<CachedObject> Foreign(int cultureId, int memberId, int objectId);
    void Create(CachedObject cachedObject);
    void Edit(CachedObject cachedObject);
    void Delete(int cultureId, int objectId);
    void Delete(CachedObject cachedObject);
  }
}