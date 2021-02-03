// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Globalization;
using System.Linq;
using Platformus.Core;
using Platformus.Core.Data.Entities;

namespace Platformus
{
  public static class DictionaryExtensions
  {
    public static string GetLocalizationValue(this Dictionary dictionary)
    {
      if (dictionary.Localizations == null)
        return string.Empty;

      return dictionary.Localizations.FirstOrDefault(l => l.CultureId == CultureInfo.CurrentCulture.TwoLetterISOLanguageName)?.Value;
    }

    public static string GetNeutralLocalizationValue(this Dictionary dictionary)
    {
      if (dictionary.Localizations == null)
        return string.Empty;

      return dictionary.Localizations.FirstOrDefault(l => l.CultureId == NeutralCulture.Id)?.Value;
    }
  }
}