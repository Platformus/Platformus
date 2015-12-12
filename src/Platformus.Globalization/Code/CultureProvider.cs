// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Globalization;
using ExtCore.Data.Abstractions;
using Platformus.Globalization.Data.Abstractions;
using Platformus.Globalization.Data.Models;

namespace Platformus.Globalization
{
  public static class CultureProvider
  {
    private static IDictionary<string, Culture> culturesByCodes;

    static CultureProvider()
    {
      CultureProvider.culturesByCodes = new Dictionary<string, Culture>();
    }

    public static Culture GetCulture(IStorage storage)
    {
      string cultureCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

      if (!CultureProvider.culturesByCodes.ContainsKey(cultureCode))
        CultureProvider.culturesByCodes[cultureCode] = storage.GetRepository<ICultureRepository>().WithCode(cultureCode);

      return CultureProvider.culturesByCodes[cultureCode];
    }
  }
}