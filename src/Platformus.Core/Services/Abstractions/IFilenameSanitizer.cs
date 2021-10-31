// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Services.Abstractions
{
  /// <summary>
  /// Describes a filename sanitizer. It should be used to ensure that filenames match the single given format
  /// and contain only allowed characters. Non-Latin strings should be automatically transliterated.
  /// </summary>
  public interface IFilenameSanitizer
  {
    /// <summary>
    /// Sanitizes a filename.
    /// </summary>
    /// <param name="filename">A filename to sanitize.</param>
    string SanitizeFilename(string filename);
  }
}