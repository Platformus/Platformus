// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.Data.Entities.Abstractions;

namespace Platformus.FileManager.Data.Entities
{
  /// <summary>
  /// Represents a file. The files are used to store the information about the files that are loaded by the users.
  /// </summary>
  public class File : IEntity
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public long Size { get; set; }
  }
}