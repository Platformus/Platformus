// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Platformus.Designers
{
  public static class FileSystemRepository
  {
    public static IEnumerable<DirectoryInfo> GetDirectories(string path)
    {
      if (!Directory.Exists(path))
        return new DirectoryInfo[] { };

      return Directory.GetDirectories(path).Select(p => new DirectoryInfo(p));
    }

    public static IEnumerable<FileInfo> GetFiles(string path, string searchPattern, string filter)
    {
      string[] paths = FileSystemRepository.GetFilteredFiles(path, searchPattern, filter);
      IEnumerable<FileInfo> files = paths.Select(p => new FileInfo(p));

      return files;
    }

    public static IEnumerable<FileInfo> GetFiles(string path, string searchPattern, string filter, string orderBy, string direction, int skip, int take)
    {
      IEnumerable<FileInfo> files = FileSystemRepository.GetFiles(path, searchPattern, filter);

      if (orderBy == "filename" && direction == "asc")
        files = files.OrderBy(f => f.Name);

      else if (orderBy == "filename" && direction == "desc")
        files = files.OrderByDescending(f => f.Name);

      return files.Skip(skip).Take(take);
    }

    public static int CountFiles(string path, string searchPattern, string filter)
    {
      return FileSystemRepository.GetFilteredFiles(path, searchPattern, filter).Count();
    }

    private static string[] GetFilteredFiles(string path, string searchPattern, string filter)
    {
      if (!Directory.Exists(path))
        return new string[] { };

      string[] paths = Directory.GetFiles(path, searchPattern);

      if (!string.IsNullOrEmpty(filter))
        paths = paths.Where(p => p.ToLower().Contains(filter.ToLower())).ToArray();

      return paths;
    }
  }
}