// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace Platformus.Forms
{
  public static class ICacheExtensions
  {
    public static IViewComponentResult GetFormViewComponentResultWithDefaultValue(this ICache cache, string code, string additionalCssClass, Func<IViewComponentResult> defaultValueFunc)
    {
      return cache.GetWithDefaultValue(
        ICacheExtensions.GetFormViewComponentResultKey(code, additionalCssClass), defaultValueFunc
      );
    }

    public static void RemoveFormViewComponentResult(this ICache cache, string code)
    {
      cache.RemoveAll(k => k.StartsWith("form-view-component:" + code + ":"));
    }

    private static string GetFormViewComponentResultKey(string code, string additionalCssClass, string cultureCode = null)
    {
      if (string.IsNullOrEmpty(cultureCode))
        cultureCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

      return "form-view-component:" + code + ":" + (string.IsNullOrEmpty(additionalCssClass) ? null : additionalCssClass + ":") + cultureCode;
    }
  }
}