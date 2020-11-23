// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Data.Entities.Abstractions;

namespace Platformus.Website.Data.Entities
{
  /// <summary>
  /// Represents a file. The files are used to store the information about the physical files that are uploaded by the users.
  /// </summary>
  public class File : IEntity<int>
  {
    /// <summary>
    /// Gets or sets the unique identifier of the file.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the file name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the file size.
    /// </summary>
    public long Size { get; set; }
  }
}