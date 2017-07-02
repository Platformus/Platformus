// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  public interface IRelationRepository : IRepository
  {
    Relation WithKey(int id);
    IEnumerable<Relation> FilteredByPrimaryId(int primaryId);
    IEnumerable<Relation> FilteredByForeignId(int foreignId);
    IEnumerable<Relation> FilteredByMemberIdAndForeignId(int memberId, int foreignId);
    void Create(Relation relation);
    void Edit(Relation relation);
    void Delete(int id);
    void Delete(Relation relation);
  }
}