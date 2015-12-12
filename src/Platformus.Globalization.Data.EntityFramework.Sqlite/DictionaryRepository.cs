// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using ExtCore.Data.EntityFramework.Sqlite;
using Microsoft.Data.Entity;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Globalization.Data.EntityFramework.Sqlite
{
  public class DictionaryRepository : RepositoryBase<Dictionary>, IDictionaryRepository
  {
    public Dictionary WithKey(int id)
    {
      return this.dbSet.FirstOrDefault(d => d.Id == id);
    }

    public void Create(Dictionary dictionary)
    {
      this.dbSet.Add(dictionary);
    }

    public void Edit(Dictionary dictionary)
    {
      this.dbContext.Entry(dictionary).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(Dictionary dictionary)
    {
      this.dbSet.Remove(dictionary);
    }
  }
}