// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Threading.Tasks;
using Platformus.Website.Data.Entities;

namespace Platformus.Website
{
  public static class ICacheExtensions
  {
    public async static Task<Menu> GetMenuWithDefaultValue(this ICache cache, string code, Func<Task<Menu>> defaultValueFunc)
    {
      return await cache.GetWithDefaultValueAsync<Menu>($"menu:{code}", defaultValueFunc);
    }

    public static void RemoveMenus(this ICache cache)
    {
      cache.RemoveAll(k => k.StartsWith("menu:"));
    }

    public async static Task<Form> GetFormWithDefaultValue(this ICache cache, string code, Func<Task<Form>> defaultValueFunc)
    {
      return await cache.GetWithDefaultValueAsync<Form>($"form:{code}", defaultValueFunc);
    }

    public static void RemoveForms(this ICache cache)
    {
      cache.RemoveAll(k => k.StartsWith("form:"));
    }
  }
}