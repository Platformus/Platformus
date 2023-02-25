// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;

namespace Platformus;

/// <summary>
/// Contains the extension methods of the <see cref="HttpRequest"/>.
/// </summary>
public static class HttpRequestBaseExtensions
{
  /// <summary>
  /// Gets the current page's URL without culture segment (if present).
  /// </summary>
  /// <param name="request">Current <see cref="HttpRequest"/> to take URL from.</param>
  public static string GetUrlWithoutCultureSegment(this HttpRequest request)
  {
    if (request.Path == "/" || request.HttpContext.GetConfigurationManager()["Globalization", "SpecifyCultureInUrl"] == "no")
      return request.Path;

    return request.Path.ToString().Substring(3);
  }

  /// <summary>
  /// Combines a new URL using the given path and current HTTP(S) request's query string
  /// according to the provided parameters.
  /// </summary>
  /// <param name="request">Current <see cref="HttpRequest"/> to take URL from.</param>
  /// <param name="parameters">Specifies which parameters should be copied from the current request,
  /// which ones should be added, replaced, or skipped.</param>
  public static string CombineUrl(this HttpRequest request, params Url.Parameter[] parameters)
  {
    return Url.Combine(request, null, parameters);
  }

  /// <summary>
  /// Combines a new URL using the given path and current HTTP(S) request's query string
  /// according to the provided parameters.
  /// </summary>
  /// <param name="request">Current <see cref="HttpRequest"/> to take URL from.</param>
  /// <param name="path">The resulting URL's path (example: '/some/new/path').</param>
  /// <param name="parameters">Specifies which parameters should be copied from the current request,
  /// which ones should be added, replaced, or skipped.</param>
  public static string CombineUrl(this HttpRequest request, string path, params Url.Parameter[] parameters)
  {
    return Url.Combine(request, path, parameters);
  }
}