// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using ExtCore.Data.Abstractions;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Entities;

namespace Platformus.Globalization
{
  public static class CultureManager
  {
    private static IEnumerable<Culture> cultures;

    public static Culture GetCulture(IStorage storage, int id)
    {
      CultureManager.CacheCultures(storage);

      return CultureManager.cultures.FirstOrDefault(c => c.Id == id);
    }

    public static Culture GetCulture(IStorage storage, string code)
    {
      CultureManager.CacheCultures(storage);

      return CultureManager.cultures.FirstOrDefault(c => string.Equals(c.Code, code, StringComparison.OrdinalIgnoreCase));
    }

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

    public static Culture GetBackendUiCulture(IStorage storage)
    {
      CultureManager.CacheCultures(storage);

      return CultureManager.cultures.FirstOrDefault(c => c.IsBackendUi);
    }

    public static Culture GetCurrentCulture(IStorage storage)
    {
      CultureManager.CacheCultures(storage);

      Culture currentCulture = CultureManager.cultures.FirstOrDefault(
        c => c.Code == CultureInfo.CurrentCulture.TwoLetterISOLanguageName
      );

      if (currentCulture == null)
        currentCulture = CultureManager.cultures.FirstOrDefault(
          c => c.Code == DefaultCulture.Code
        );

      return currentCulture;
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