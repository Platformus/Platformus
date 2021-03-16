// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Globalization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Core.Data.Entities;
using Platformus.Core.Extensions;
using Platformus.Core.Services.Abstractions;

namespace Platformus.Core.Frontend
{
  public static class GlobalizedUrlFormatter
  {
    public static string Format(HttpContext httpContext, string url)
    {
      bool specifyCultureInUrl = httpContext.RequestServices.GetService<IConfigurationManager>()["Globalization", "SpecifyCultureInUrl"] == "yes";

      if (!specifyCultureInUrl)
        return url;

      Culture defaultCulture = httpContext.GetCultureManager().GetFrontendDefaultCultureAsync().Result;

      if (defaultCulture.Id == CultureInfo.CurrentCulture.TwoLetterISOLanguageName && url == "/")
        return url;

      return string.Format("/{0}{1}", CultureInfo.CurrentCulture.TwoLetterISOLanguageName, url);
    }
  }
}