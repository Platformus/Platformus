// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace Platformus.Barebone
{
  internal class HttpExceptionMiddleware
  {
    private readonly RequestDelegate next;

    public HttpExceptionMiddleware(RequestDelegate next)
    {
      this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
      try
      {
        await this.next.Invoke(context);
      }

      catch (HttpException httpException)
      {
        context.Response.StatusCode = httpException.StatusCode;

        IHttpResponseFeature httpResponseFeature = context.Features.Get<IHttpResponseFeature>();

        httpResponseFeature.ReasonPhrase = httpException.Message;
      }
    }
  }
}