// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc;

namespace Platformus.Globalization
{
  public static class ICacheExtensions
  {
    public static IViewComponentResult GetCulturesViewComponentResultWithDefaultValue(this ICache cache, string additionalCssClass, Func<IViewComponentResult> defaultValueFunc)
    {
      return cache.GetWithDefaultValue(
        ICacheExtensions.GetCulturesViewComponentResultKey(additionalCssClass), defaultValueFunc
      );
    }

    public static void RemoveCulturesViewComponentResult(this ICache cache)
    {
      cache.RemoveAll(k => k.StartsWith("cultures-view-component"));
    }

    private static string GetCulturesViewComponentResultKey(string additionalCssClass)
    {
      return "cultures-view-component" + (string.IsNullOrEmpty(additionalCssClass) ? null : ":" + additionalCssClass);
    }
  }
}