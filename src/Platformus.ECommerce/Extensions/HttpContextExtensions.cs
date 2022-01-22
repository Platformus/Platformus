using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Platformus.ECommerce.Services.Abstractions;

namespace Platformus.ECommerce
{
  /// <summary>
  /// Contains the extension methods of the <see cref="HttpContext"/>.
  /// </summary>
  public static class HttpContextExtensions
  {
    /// <summary>
    /// Gets a cart manager service.
    /// </summary>
    /// <param name="httpContext">Current <see cref="HttpContext"/> to get the service from.</param>
    public static ICartManager GetCartManager(this HttpContext httpContext)
    {
      return httpContext.RequestServices.GetService<ICartManager>();
    }
  }
}
