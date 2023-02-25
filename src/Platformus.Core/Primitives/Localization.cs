// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Primitives;

/// <summary>
/// Describes a localization (a localized version of a string with a given culture).
/// </summary>
public class Localization
{
  /// <summary>
  /// A culture the localization belongs to.
  /// </summary>
  public Culture Culture { get; }

  /// <summary>
  /// A localized version of a string.
  /// </summary>
  public string Value { get; }

  /// <summary>
  /// Initializes a new instance of the <see cref="Localization"/> class.
  /// </summary>
  /// <param name="culture">A culture the localization belongs to.</param>
  /// <param name="value">A localized version of a string.</param>
  public Localization(Culture culture, string value = null)
  {
    this.Culture = culture;
    this.Value = value;
  }
}