using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core;
using Platformus.Core.Extensions;
using Platformus.Website.Endpoints;
using Platformus.Website.Frontend.Services.Abstractions;

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
        // Check if caching is used

        byte[] responseBody = await new MemoryResponseCache().GetWithDefaultValueAsync(
          httpContext,
          async () =>
          {
            IActionResult actionResult = await this.CreateEndpointInstance(endpoint).InvokeAsync(httpContext, endpoint);

            if (actionResult == null)
              return null;

            return await this.GetResponseBodyAsync(httpContext, actionResult);
          }
        );

        if (responseBody != null)
        {
          await httpContext.Response.Body.WriteAsync(responseBody, 0, responseBody.Length);
          return;
        }
      }

      await this.next(httpContext);
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