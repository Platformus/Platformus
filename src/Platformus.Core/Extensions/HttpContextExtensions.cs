// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

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
  /// <summary>
  /// Contains the extension methods of the <see cref="HttpContext"/>.
  /// </summary>
  public static class HttpContextExtensions
  {
    private static readonly RouteData EmptyRouteData = new RouteData();
    private static readonly ActionDescriptor EmptyActionDescriptor = new ActionDescriptor();

    /// <summary>
    /// Gets a string localizer service for the given type.
    /// </summary>
    /// <typeparam name="T">A type to get the string localizer of.</typeparam>
    /// <param name="httpContext">Current <see cref="HttpContext"/> to get the service from.</param>
    public static IStringLocalizer<T> GetStringLocalizer<T>(this HttpContext httpContext)
    {
      return httpContext.RequestServices.GetService<IStringLocalizer<T>>();
    }

    /// <summary>
    /// Gets a storage service.
    /// </summary>
    /// <param name="httpContext">Current <see cref="HttpContext"/> to get the service from.</param>
    public static IStorage GetStorage(this HttpContext httpContext)
    {
      return httpContext.RequestServices.GetService<IStorage>();
    }

    /// <summary>
    /// Gets a cache service.
    /// </summary>
    /// <param name="httpContext">Current <see cref="HttpContext"/> to get the service from.</param>
    public static ICache GetCache(this HttpContext httpContext)
    {
      return httpContext.RequestServices.GetService<ICache>();
    }

    /// <summary>
    /// Gets a configuration manager service.
    /// </summary>
    /// <param name="httpContext">Current <see cref="HttpContext"/> to get the service from.</param>
    public static IConfigurationManager GetConfigurationManager(this HttpContext httpContext)
    {
      return httpContext.RequestServices.GetService<IConfigurationManager>();
    }

    /// <summary>
    /// Gets a culture manager service.
    /// </summary>
    /// <param name="httpContext">Current <see cref="HttpContext"/> to get the service from.</param>
    public static ICultureManager GetCultureManager(this HttpContext httpContext)
    {
      return httpContext.RequestServices.GetService<ICultureManager>();
    }

    /// <summary>
    /// Gets a user manager service.
    /// </summary>
    /// <param name="httpContext">Current <see cref="HttpContext"/> to get the service from.</param>
    public static IUserManager GetUserManager(this HttpContext httpContext)
    {
      return httpContext.RequestServices.GetService<IUserManager>();
    }

    /// <summary>
    /// Gets the localizations for all the non-neutral cultures for a given dictionary.
    /// If the dictionary doesn't contain a localization for a specific culture, an empty localization will be created.
    /// If the dictionary is null, new localizations will be created for all the non-neutral cultures.
    /// </summary>
    /// <param name="httpContext">Current <see cref="HttpContext"/> to get the culture manager service from.</param>
    /// <param name="dictionary">A <see cref="Dictionary"/> to get the localizations from.</param>
    /// <returns></returns>
    public static IEnumerable<Core.Primitives.Localization> GetLocalizations(this HttpContext httpContext, Dictionary dictionary = null)
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

    /// <summary>
    /// Creates a sorting parameter value string for a localized property.
    /// Nested localized properties are supported to (example: "Category.Name").
    /// </summary>
    /// <param name="httpContext"></param>
    /// <param name="propertyName"></param>
    /// <returns></returns>
    public static string CreateLocalizedSorting(this HttpContext httpContext, string propertyName)
    {
      return $"{propertyName}.Localizations.First(l=>l.Culture.Id=\"{CultureInfo.CurrentUICulture.TwoLetterISOLanguageName}\").Value";
    }

    /// <summary>
    /// Executes an action result and writes it into the current <see cref="HttpContext"/> response body.
    /// </summary>
    /// <param name="httpContext">Current <see cref="HttpContext"/> to write the response body.</param>
    /// <param name="result">An action result that should be executed.</param>
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
