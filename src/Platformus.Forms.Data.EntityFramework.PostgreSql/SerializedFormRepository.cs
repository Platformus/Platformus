// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Forms.Data.Abstractions;
using Platformus.Forms.Data.Entities;

namespace Platformus.Forms.Data.EntityFramework.PostgreSql
{
  public class SerializedFormRepository : RepositoryBase<SerializedForm>, ISerializedFormRepository
  {
    public SerializedForm WithKey(int cultureId, int formId)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(sf => sf.CultureId == cultureId && sf.FormId == formId);
    }

    public SerializedForm WithCultureIdAndCode(int cultureId, string code)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(sf => sf.CultureId == cultureId && string.Equals(sf.Code, code, System.StringComparison.OrdinalIgnoreCase));
    }

    public void Create(SerializedForm serializedForm)
    {
      this.dbSet.Add(serializedForm);
    }

    public void Edit(SerializedForm serializedForm)
    {
      this.storageContext.Entry(serializedForm).State = EntityState.Modified;
    }

    public void Delete(int cultureId, int formId)
    {
      this.Delete(this.WithKey(cultureId, formId));
    }

    public void Delete(SerializedForm serializedForm)
    {
      this.dbSet.Remove(serializedForm);
    }
  }
}