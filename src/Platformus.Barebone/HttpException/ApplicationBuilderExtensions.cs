// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Builder;

namespace Platformus.Barebone
{
  public static class ApplicationBuilderExtensions
  {
    public static IApplicationBuilder UseHttpException(this IApplicationBuilder applicationBuilder)
    {
      return applicationBuilder.UseMiddleware<HttpExceptionMiddleware>();
    }
  }
}