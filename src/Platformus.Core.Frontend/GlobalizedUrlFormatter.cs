// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Globalization;
using Microsoft.AspNetCore.Http;
using Platformus.Core.Data.Entities;

namespace Platformus.Core.Frontend;

// TODO: consider moving to HttpContextExtensions
public static class GlobalizedUrlFormatter
{
  public static string Format(HttpContext httpContext, string url)
  {
    bool specifyCultureInUrl = httpContext.GetConfigurationManager()["Globalization", "SpecifyCultureInUrl"] == "yes";

    if (!specifyCultureInUrl)
      return url;

    Culture defaultCulture = httpContext.GetCultureManager().GetFrontendDefaultCultureAsync().Result;

    if (defaultCulture.Id == CultureInfo.CurrentCulture.TwoLetterISOLanguageName && url == "/")
      return url;

    return string.Format("/{0}{1}", CultureInfo.CurrentCulture.TwoLetterISOLanguageName, url);
  }
}