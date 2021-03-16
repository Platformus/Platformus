using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Platformus.ECommerce.Services.Abstractions;

namespace Platformus.ECommerce.Extensions
{
  public static class HttpContextExtensions
  {
    public static ICartManager GetCartManager(this HttpContext httpContext)
    {
      return httpContext.RequestServices.GetService<ICartManager>();
    }
  }
}
