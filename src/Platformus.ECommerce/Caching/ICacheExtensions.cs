// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc;

namespace Platformus.ECommerce
{
  public static class ICacheExtensions
  {
    public static IViewComponentResult GetCatalogsViewComponentResultWithDefaultValue(this ICache cache, string additionalCssClass, Func<IViewComponentResult> defaultValueFunc)
    {
      return cache.GetWithDefaultValue(
        ICacheExtensions.GetCatalogsViewComponentResultKey(additionalCssClass), defaultValueFunc
      );
    }

    public static IViewComponentResult GetCartViewComponentResultWithDefaultValue(this ICache cache, string additionalCssClass, Func<IViewComponentResult> defaultValueFunc)
    {
      return cache.GetWithDefaultValue(
        ICacheExtensions.GetCartViewComponentResultKey(additionalCssClass), defaultValueFunc
      );
    }

    public static void RemoveCatalogsViewComponentResult(this ICache cache)
    {
      cache.RemoveAll(k => k.StartsWith("catalogs-view-component"));
    }

    public static void RemoveCartViewComponentResult(this ICache cache)
    {
      cache.RemoveAll(k => k.StartsWith("cart-view-component"));
    }

    private static string GetCatalogsViewComponentResultKey(string additionalCssClass)
    {
      return "catalogs-view-component" + (string.IsNullOrEmpty(additionalCssClass) ? null : ":" + additionalCssClass);
    }

    private static string GetCartViewComponentResultKey(string additionalCssClass)
    {
      return "cart-view-component" + (string.IsNullOrEmpty(additionalCssClass) ? null : ":" + additionalCssClass);
    }
  }
}