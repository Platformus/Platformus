using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Platformus.Core.Data.Entities;
using Platformus.Core.Services.Abstractions;

namespace Platformus
{
  public static class HttpContextExtensions
  {
    private static readonly RouteData EmptyRouteData = new RouteData();
    private static readonly ActionDescriptor EmptyActionDescriptor = new ActionDescriptor();

    public static IStringLocalizer<T> GetStringLocalizer<T>(this HttpContext httpContext)
    {
      return httpContext.RequestServices.GetService<IStringLocalizer<T>>();
    }

    public static IStorage GetStorage(this HttpContext httpContext)
    {
      return httpContext.RequestServices.GetService<IStorage>();
    }

    public static ICache GetCache(this HttpContext httpContext)
    {
      return httpContext.RequestServices.GetService<ICache>();
    }

    public static IConfigurationManager GetConfigurationManager(this HttpContext httpContext)
    {
      return httpContext.RequestServices.GetService<IConfigurationManager>();
    }

    public static ICultureManager GetCultureManager(this HttpContext httpContext)
    {
      return httpContext.RequestServices.GetService<ICultureManager>();
    }

    public static IUserManager GetUserManager(this HttpContext httpContext)
    {
      return httpContext.RequestServices.GetService<IUserManager>();
    }

    public static IEnumerable<Core.Primitives.Localization> GetLocalizations(this HttpContext httpContext, Core.Data.Entities.Dictionary dictionary = null)
    {
      List<Core.Primitives.Localization> localizations = new List<Core.Primitives.Localization>();

      foreach (Culture culture in httpContext.GetCultureManager().GetCulturesAsync().Result)
      {
        Core.Primitives.Localization localization;

        if (dictionary == null)
          localization = new Core.Primitives.Localization(
            new Core.Primitives.Culture(culture.Id)
          );

        else localization = new Core.Primitives.Localization(
          new Core.Primitives.Culture(culture.Id),
          dictionary.Localizations.FirstOrDefault(l => l.CultureId == culture.Id)?.Value
        );

        localizations.Add(localization);
      }

      return localizations;
    }

    public static string CreateLocalizedSorting(this HttpContext httpContext, string propertyName)
    {
      return $"{propertyName}.Localizations.First(l=>l.Culture.Id=\"{CultureInfo.CurrentUICulture.TwoLetterISOLanguageName}\").Value";
    }

    public static async Task ExecuteResultAsync(this HttpContext httpContext, IActionResult result)
    {
      if (httpContext == null)
        throw new ArgumentNullException(nameof(httpContext));

      if (result == null)
        throw new ArgumentNullException(nameof(result));

      RouteData routeData = httpContext.GetRouteData() ?? HttpContextExtensions.EmptyRouteData;
      ActionContext actionContext = new ActionContext(httpContext, routeData, HttpContextExtensions.EmptyActionDescriptor);

      if (result is ViewResult)
        await httpContext.RequestServices.GetRequiredService<IActionResultExecutor<ViewResult>>().ExecuteAsync(actionContext, result as ViewResult);
    }
  }
}
