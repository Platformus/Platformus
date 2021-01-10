using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core;
using Platformus.Core.Extensions;
using Platformus.Website.Endpoints;
using Platformus.Website.Frontend.Services.Abstractions;
using Platformus.Website.ResponseCaches;

namespace Platformus.Website.Frontend.Middleware
{
  public class WebsiteMiddleware
  {
    private readonly RequestDelegate next;

    public WebsiteMiddleware(RequestDelegate next)
    {
      this.next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext, IEndpointResolver endpointResolver)
    {
      Data.Entities.Endpoint endpoint = await endpointResolver.ResolveAsync(httpContext);

      if (endpoint != null)
      {
        if (!endpoint.DisallowAnonymous || (httpContext.User.Identity.IsAuthenticated && endpoint.EndpointPermissions.All(ep => httpContext.User.HasClaim(PlatformusClaimTypes.Permission, ep.Permission.Code))))
        {
          byte[] responseBody;
          Func<Task<byte[]>> defaultValueFunc = async () =>
          {
            IActionResult actionResult = await this.CreateEndpointInstance(endpoint).InvokeAsync(httpContext, endpoint);

            if (actionResult == null)
              return null;

            return await this.GetResponseBodyAsync(httpContext, actionResult);
          };

          if (string.IsNullOrEmpty(endpoint.ResponseCacheCSharpClassName))
            responseBody = await defaultValueFunc();

          else
          {
            responseBody = await this.CreateResponseCacheInstance(endpoint).GetWithDefaultValueAsync(
              httpContext,
              defaultValueFunc
            );
          }

          if (responseBody != null)
          {
            await httpContext.Response.Body.WriteAsync(responseBody, 0, responseBody.Length);
            return;
          }
        }
      }

      await this.next(httpContext);
    }

    private IResponseCache CreateResponseCacheInstance(Data.Entities.Endpoint endpoint)
    {
      return StringActivator.CreateInstance<IResponseCache>(endpoint.ResponseCacheCSharpClassName);
    }

    private IEndpoint CreateEndpointInstance(Data.Entities.Endpoint endpoint)
    {
      return StringActivator.CreateInstance<IEndpoint>(endpoint.CSharpClassName);
    }

    private async Task<byte[]> GetResponseBodyAsync(HttpContext httpContext, IActionResult actionResult)
    {
      Stream responseBody = httpContext.Response.Body;

      using (MemoryStream buffer = new MemoryStream())
      {
        try
        {
          httpContext.Response.Body = buffer;
          await httpContext.ExecuteResultAsync(actionResult);
        }

        finally
        {
          httpContext.Response.Body = responseBody;
        }

        return buffer.ToArray();
      }
    }
  }
}