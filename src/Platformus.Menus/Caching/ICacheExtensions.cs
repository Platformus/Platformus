// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace Platformus.Menus
{
  public static class ICacheExtensions
  {
    public static IViewComponentResult GetMenuViewComponentResultWithDefaultValue(this ICache cache, string code, string additionalCssClass, Func<IViewComponentResult> defaultValueFunc)
    {
      return cache.GetWithDefaultValue(
        ICacheExtensions.GetMenuViewComponentResultKey(code, additionalCssClass), defaultValueFunc
      );
    }

    public static void RemoveMenuViewComponentResult(this ICache cache, string code)
    {
      cache.RemoveAll(k => k.StartsWith("menu-view-component:" + code + ":"));
    }

    private static string GetMenuViewComponentResultKey(string code, string additionalCssClass, string cultureCode = null)
    {
      if (string.IsNullOrEmpty(cultureCode))
        cultureCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

      return "menu-view-component:" + code + ":" + (string.IsNullOrEmpty(additionalCssClass) ? null : additionalCssClass + ":") + cultureCode;
    }
  }
}