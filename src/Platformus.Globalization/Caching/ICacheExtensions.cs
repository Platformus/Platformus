// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;

namespace Platformus.Globalization
{
  public static class ICacheExtensions
  {
    public static IViewComponentResult GetCulturesViewComponentResultWithDefaultValue(this ICache cache, Func<IViewComponentResult> defaultValueFunc)
    {
      return cache.GetWithDefaultValue(
        ICacheExtensions.GetCulturesViewComponentResultKey(), defaultValueFunc
      );
    }

    public static void RemoveCulturesViewComponentResult(this ICache cache, string code, string cultureCode)
    {
      cache.Remove(ICacheExtensions.GetCulturesViewComponentResultKey());
    }

    private static string GetCulturesViewComponentResultKey()
    {
      return "cultures-view-component";
    }
  }
}