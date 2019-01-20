// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.Abstractions;
using Platformus.Domain.Data.Abstractions;
using Platformus.Domain.Data.Entities;
using Platformus.Domain.Services.Abstractions;

namespace Platformus.Domain.Services.Defaults
{
  public class DefaultDomainManager : IDomainManager
  {
    private ICache cache;
    private IStorage storage;

    public DefaultDomainManager(ICache cache, IStorage storage)
    {
      this.cache = cache;
      this.storage = storage;
    }

    public DataType GetDataType(int id)
    {
      return this.GetCachedDataTypes().FirstOrDefault(dt => dt.Id == id);
    }

    public IEnumerable<DataType> GetDataTypes()
    {
      return this.GetCachedDataTypes();
    }

    public Class GetClass(int id)
    {
      return this.GetCachedClasses().FirstOrDefault(c => c.Id == id);
    }

    public Class GetClass(string code)
    {
      return this.GetCachedClasses().FirstOrDefault(c => string.Equals(c.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    public Member GetMember(int id)
    {
      return this.GetCachedMembers().FirstOrDefault(dt => dt.Id == id);
    }

    public Member GetMemberByClassIdAndCodeInlcudingParent(int classId, string code)
    {
      return this.GetMembersByClassIdInlcudingParent(classId).FirstOrDefault(m => string.Equals(m.Code, code, StringComparison.OrdinalIgnoreCase));
    }

    public IEnumerable<Member> GetMembersByClassIdInlcudingParent(int classId)
    {
      return this.GetCachedMembers().Where(m => m.ClassId == classId || m.ClassId == this.GetClass(classId).ClassId);
    }

    public void InvalidateCache()
    {
      this.cache.Remove("data-types");
      this.cache.Remove("classes");
      this.cache.Remove("members");
    }

    private IEnumerable<DataType> GetCachedDataTypes()
    {
      return this.cache.GetWithDefaultValue<IEnumerable<DataType>>(
        "data-types",
        () => this.storage.GetRepository<IDataTypeRepository>().All().ToList(),
        new CacheEntryOptions(priority: CacheEntryPriority.NeverRemove)
      );
    }

    private IEnumerable<Class> GetCachedClasses()
    {
      return this.cache.GetWithDefaultValue<IEnumerable<Class>>(
        "classes",
        () => this.storage.GetRepository<IClassRepository>().All().ToList(),
        new CacheEntryOptions(priority: CacheEntryPriority.NeverRemove)
      );
    }

    private IEnumerable<Member> GetCachedMembers()
    {
      return this.cache.GetWithDefaultValue<IEnumerable<Member>>(
        "members",
        () => this.storage.GetRepository<IMemberRepository>().All().ToList(),
        new CacheEntryOptions(priority: CacheEntryPriority.NeverRemove)
      );
    }
  }
}