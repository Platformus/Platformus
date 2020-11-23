// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Data.Entities;
using Platformus.Core.Extensions;

namespace Platformus
{
  public static class DictionaryExtensions
  {
    public static string GetLocalizationValue(this Dictionary dictionary, HttpContext httpContext)
    {
      if (dictionary.Localizations == null)
        return string.Empty;

      return dictionary.Localizations.FirstOrDefault(l => l.CultureId == httpContext.GetCultureManager().GetCurrentCultureAsync().Result.Id)?.Value;
    }

    public static string GetNeutralLocalizationValue(this Dictionary dictionary, HttpContext httpContext)
    {
      if (dictionary.Localizations == null)
        return string.Empty;

      return dictionary.Localizations.FirstOrDefault(l => l.CultureId == httpContext.GetCultureManager().GetNeutralCultureAsync().Result.Id)?.Value;
    }
  }
}