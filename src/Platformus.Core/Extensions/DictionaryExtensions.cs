// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Globalization;
using System.Linq;
using Platformus.Core;
using Platformus.Core.Data.Entities;

namespace Platformus
{
  /// <summary>
  /// Contains the extension methods of the <see cref="Dictionary"/>.
  /// </summary>
  public static class DictionaryExtensions
  {
    /// <summary>
    /// Gets a localized string value for the current culture.
    /// </summary>
    /// <param name="dictionary">A <see cref="Dictionary"/> to get the localization from.</param>
    public static string GetLocalizationValue(this Dictionary dictionary)
    {
      if (dictionary?.Localizations == null)
        return string.Empty;

      return dictionary.Localizations.FirstOrDefault(l => l.CultureId == CultureInfo.CurrentCulture.TwoLetterISOLanguageName)?.Value;
    }

    /// <summary>
    /// Gets a localized string value for the neutral culture.
    /// </summary>
    /// <param name="dictionary">A <see cref="Dictionary"/> to get the localization from.</param>
    public static string GetNeutralLocalizationValue(this Dictionary dictionary)
    {
      if (dictionary?.Localizations == null)
        return string.Empty;

      return dictionary.Localizations.FirstOrDefault(l => l.CultureId == NeutralCulture.Id)?.Value;
    }
  }
}