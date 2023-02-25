// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Services.Abstractions;

/// <summary>
/// Describes a non-Latyn strings transliterator.
/// </summary>
public interface ITransliterator
{
  /// <summary>
  /// Transliterates a non-Latin string.
  /// </summary>
  /// <param name="text">A non-Latin string to transliterate.</param>
  string Transliterate(string text);
}