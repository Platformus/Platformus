// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using ExtCore.Data.Abstractions;
using Platformus.FileManager.Data.Entities;

namespace Platformus.FileManager.Data.Abstractions
{
  public interface IFileRepository : IRepository
  {
    File WithKey(int id);
    IEnumerable<File> All();
    IEnumerable<File> Range(string orderBy, string direction, int skip, int take, string filter);
    void Create(File file);
    void Edit(File file);
    void Delete(int id);
    void Delete(File file);
    int Count(string filter);
  }
}