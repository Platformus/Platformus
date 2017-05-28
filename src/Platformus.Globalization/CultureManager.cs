// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ExtCore.Data.Abstractions;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Globalization
{
  public static class CultureManager
  {
    private static IEnumerable<Culture> cultures;

    public static Culture GetNeutralCulture(IStorage storage)
    {
      CultureManager.CacheCultures(storage);

      return CultureManager.cultures.FirstOrDefault(c => c.IsNeutral);
    }

    public static Culture GetDefaultCulture(IStorage storage)
    {
      CultureManager.CacheCultures(storage);

      return CultureManager.cultures.FirstOrDefault(c => c.IsDefault);
    }

    public static Culture GetCurrentCulture(IStorage storage)
    {
      CultureManager.CacheCultures(storage);

      return CultureManager.cultures.FirstOrDefault(
        c => c.Code == CultureInfo.CurrentCulture.TwoLetterISOLanguageName
      );
    }

    public static IEnumerable<Culture> GetCultures(IStorage storage)
    {
      CultureManager.CacheCultures(storage);

      return CultureManager.cultures.OrderBy(c => c.Name);
    }

    public static IEnumerable<Culture> GetNotNeutralCultures(IStorage storage)
    {
      CultureManager.CacheCultures(storage);

      return CultureManager.cultures.Where(c => !c.IsNeutral).OrderBy(c => c.Name);
    }

    public static void InvalidateCache()
    {
      CultureManager.cultures = null;
    }

    private static void CacheCultures(IStorage storage)
    {
      if (CultureManager.cultures == null)
        CultureManager.cultures = storage.GetRepository<ICultureRepository>().All().ToList();
    }
  }
}