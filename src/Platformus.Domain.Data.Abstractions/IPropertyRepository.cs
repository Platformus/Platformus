// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Data.Abstractions
{
  public interface IPropertyRepository : IRepository
  {
    Property WithKey(int id);
    Property WithObjectIdAndMemberId(int objectId, int memberId);
    IEnumerable<Property> FilteredByObjectId(int objectId);
    void Create(Property property);
    void Edit(Property property);
    void Delete(int id);
    void Delete(Property property);
  }
}