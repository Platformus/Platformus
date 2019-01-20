// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Globalization;
using Platformus.Barebone;
using Platformus.Configurations.Services.Abstractions;

namespace Platformus.Globalization.Frontend
{
  public static class GlobalizedUrlFormatter
  {
    public static string Format(IRequestHandler requestHandler, string url)
    {
      bool specifyCultureInUrl = requestHandler.GetService<IConfigurationManager>()["Globalization", "SpecifyCultureInUrl"] != "no";

      if (specifyCultureInUrl)
        return string.Format("/{0}{1}", CultureInfo.CurrentCulture.TwoLetterISOLanguageName, url);

      return url;
    }
  }
}