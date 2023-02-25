// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;
using ExtCore.Infrastructure;
using Platformus.Core.Services.Abstractions;

namespace Platformus.Core.Services.Defaults;

public class DefaultFilenameSanitizer : IFilenameSanitizer
{
  public string SanitizeFilename(string filename)
  {
    filename = filename.ToLower();

    foreach (ITransliterator transliterator in ExtensionManager.GetInstances<ITransliterator>())
      filename = transliterator.Transliterate(filename);

    return string.Join("_", filename.Split(Path.GetInvalidFileNameChars()))
      .Replace(' ', '_');
  }
}