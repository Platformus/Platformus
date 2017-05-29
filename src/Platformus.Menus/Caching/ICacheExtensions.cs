// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace Platformus.Menus
{
  public static class ICacheExtensions
  {
    public static IViewComponentResult GetMenuViewComponentResultWithDefaultValue(this ICache cache, string code, Func<IViewComponentResult> defaultValueFunc)
    {
      return cache.GetWithDefaultValue(
        ICacheExtensions.GetMenuViewComponentResultKey(code), defaultValueFunc
      );
    }

    public static void RemoveMenuViewComponentResult(this ICache cache, string code)
    {
      cache.Remove(ICacheExtensions.GetMenuViewComponentResultKey(code));
    }

    private static string GetMenuViewComponentResultKey(string code)
    {
      return "mvcr:" + code + ":" + CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
    }
  }
}