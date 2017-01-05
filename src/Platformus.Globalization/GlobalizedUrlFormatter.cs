// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Globalization;
using ExtCore.Data.Abstractions;
using Platformus.Configurations;

namespace Platformus.Globalization
{
  public static class GlobalizedUrlFormatter
  {
    public static string Format(IStorage storage, string url)
    {
      bool specifyCultureInUrl = new ConfigurationManager(storage)["Globalization", "SpecifyCultureInUrl"] != "no";

      if (specifyCultureInUrl)
        return string.Format("/{0}{1}", CultureInfo.CurrentCulture.TwoLetterISOLanguageName, url);

      return url;
    }
  }
}