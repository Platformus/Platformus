// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Menus.Data.Abstractions;
using Platformus.Menus.Data.Entities;

namespace Platformus.Menus.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="ISerializedMenuRepository"/> interface and represents the repository
  /// for manipulating the <see cref="SerializedMenu"/> entities in the context of SQL Server database.
  /// </summary>
  public class SerializedMenuRepository : RepositoryBase<SerializedMenu>, ISerializedMenuRepository
  {
    public SerializedMenu WithKey(int cultureId, int menuId)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(sm => sm.CultureId == cultureId && sm.MenuId == menuId);
    }

    public SerializedMenu WithCultureIdAndCode(int cultureId, string code)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(sm => sm.CultureId == cultureId && string.Equals(sm.Code, code, System.StringComparison.OrdinalIgnoreCase));
    }

    public void Create(SerializedMenu serializedMenu)
    {
      this.dbSet.Add(serializedMenu);
    }

    public void Edit(SerializedMenu serializedMenu)
    {
      this.storageContext.Entry(serializedMenu).State = EntityState.Modified;
    }

    public void Delete(int cultureId, int menuId)
    {
      this.Delete(this.WithKey(cultureId, menuId));
    }

    public void Delete(SerializedMenu serializedMenu)
    {
      this.dbSet.Remove(serializedMenu);
    }
  }
}