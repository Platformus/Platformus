// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain.Services.Abstractions
{
  public interface IDomainManager
  {
    DataType GetDataType(int id);
    IEnumerable<DataType> GetDataTypes();
    Class GetClass(int id);
    Class GetClass(string code);
    Member GetMember(int id);
    Member GetMemberByClassIdAndCodeInlcudingParent(int classId, string code);
    IEnumerable<Member> GetMembersByClassIdInlcudingParent(int classId);
    void InvalidateCache();
  }
}