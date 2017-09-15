// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using ExtCore.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Platformus.Barebone.Data.Extensions;
using Platformus.FileManager.Data.Abstractions;
using Platformus.FileManager.Data.Entities;

namespace Platformus.FileManager.Data.EntityFramework.SqlServer
{
  /// <summary>
  /// Implements the <see cref="IFileRepository"/> interface and represents the repository
  /// for manipulating the <see cref="File"/> entities in the context of SQL Server database.
  /// </summary>
  public class FileRepository : RepositoryBase<File>, IFileRepository
  {
    public File WithKey(int id)
    {
      return this.dbSet.AsNoTracking().FirstOrDefault(f => f.Id == id);
    }

    public IEnumerable<File> All()
    {
      return this.dbSet.AsNoTracking().OrderBy(f => f.Name);
    }

    public IEnumerable<File> Range(string orderBy, string direction, int skip, int take, string filter)
    {
      return this.GetFilteredFiles(this.dbSet.AsNoTracking(), filter).OrderBy(orderBy, direction).Skip(skip).Take(take);
    }

    public void Create(File file)
    {
      this.dbSet.Add(file);
    }

    public void Edit(File file)
    {
      this.storageContext.Entry(file).State = EntityState.Modified;
    }

    public void Delete(int id)
    {
      this.Delete(this.WithKey(id));
    }

    public void Delete(File file)
    {
      this.dbSet.Remove(file);
    }

    public int Count(string filter)
    {
      return this.GetFilteredFiles(this.dbSet, filter).Count();
    }

    private IQueryable<File> GetFilteredFiles(IQueryable<File> files, string filter)
    {
      if (string.IsNullOrEmpty(filter))
        return files;

      return files.Where(f => f.Name.ToLower().Contains(filter.ToLower()));
    }
  }
}