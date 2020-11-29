// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Platformus.ECommerce.Data.Entities;

namespace Platformus.ECommerce
{
  public static class ICacheExtensions
  {
    public async static Task<IEnumerable<Catalog>> GetCatalogsWithDefaultValue(this ICache cache, Func<Task<IEnumerable<Catalog>>> defaultValueFunc)
    {
      return await cache.GetWithDefaultValueAsync<IEnumerable<Catalog>>("catalogs", defaultValueFunc);
    }

    public static void RemoveMenus(this ICache cache)
    {
      cache.RemoveAll(k => k == "catalogs");
    }
  }
}