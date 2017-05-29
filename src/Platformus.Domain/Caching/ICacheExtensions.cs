// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace Platformus.Domain
{
  public static class ICacheExtensions
  {
    public static IActionResult GetPageActionResultWithDefaultValue(this ICache cache, string url, Func<IActionResult> defaultValueFunc)
    {
      return cache.GetWithDefaultValue(
        ICacheExtensions.GetPageActionResultKey(url), defaultValueFunc
      );
    }

    public static void RemovePageActionResult(this ICache cache, string url)
    {
      cache.Remove(ICacheExtensions.GetPageActionResultKey(url));
    }

    private static string GetPageActionResultKey(string url)
    {
      return "par:" + url + ":" + CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
    }
  }
}