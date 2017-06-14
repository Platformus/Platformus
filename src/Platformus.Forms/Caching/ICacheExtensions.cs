// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace Platformus.Forms
{
  public static class ICacheExtensions
  {
    public static IViewComponentResult GetFormViewComponentResultWithDefaultValue(this ICache cache, string code, Func<IViewComponentResult> defaultValueFunc)
    {
      return cache.GetWithDefaultValue(
        ICacheExtensions.GetFormViewComponentResultKey(code), defaultValueFunc
      );
    }

    public static void RemoveFormViewComponentResult(this ICache cache, string code, string cultureCode)
    {
      cache.Remove(ICacheExtensions.GetFormViewComponentResultKey(code, cultureCode));
    }

    private static string GetFormViewComponentResultKey(string code, string cultureCode = null)
    {
      return "fvcr:" + code + ":" + (string.IsNullOrEmpty(cultureCode) ? CultureInfo.CurrentCulture.TwoLetterISOLanguageName : cultureCode);
    }
  }
}