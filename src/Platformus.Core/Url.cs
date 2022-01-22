// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Platformus
{
  /// <summary>
  /// Helps to build the URLs using the current HTTP(S) request's query string.
  /// </summary>
  public class Url
  {
    /// <summary>
    /// Specifies which parameters should be copied from the current request,
    /// which ones should be added, replaced, or skipped.
    /// </summary>
    public class Parameter
    {
      /// <summary>
      /// Gets or sets the parameter's name.
      /// </summary>
      public string Name { get; set; }

      /// <summary>
      /// Gets or sets the parameter's value (if parameter should be added or replaced).
      /// </summary>
      public string Value { get; set; }

      /// <summary>
      /// Gets or sets value indicating that the parameter should be copied from the current HTTP(S) request's query string.
      /// </summary>
      public bool TakeFromUrl { get; set; }

      /// <summary>
      /// Gets or sets value indicating that the parameter should be skipped from the current HTTP(S) request's query string.
      /// </summary>
      public bool Skip { get; set; }

      /// <summary>
      /// Initializes a new instance of the <see cref="Parameter"/> class.
      /// </summary>
      /// <param name="name">The parameter's name.</param>
      /// <param name="value">The parameter's value (if parameter should be added or replaced).</param>
      /// <param name="takeFromUrl">A value indicating that the parameter should be copied from the current HTTP(S) request's query string.</param>
      /// <param name="skip">A value indicating that the parameter should be skipped from the current HTTP(S) request's query string.</param>
      public Parameter(string name = null, object value = null, bool takeFromUrl = false, bool skip = false)
      {
        this.Name = name;
        this.Value = value == null ? null : value.ToString();
        this.TakeFromUrl = takeFromUrl;
        this.Skip = skip;
      }
    }

    /// <summary>
    /// Combines a new URL using the given path and current HTTP(S) request's query string
    /// according to the provided parameters.
    /// </summary>
    /// <param name="request">The current HTTP(S) request.</param>
    /// <param name="path">The resulting URL's path (example: '/some/new/path').</param>
    /// <param name="parameters">Specifies which parameters should be copied from the current request,
    /// which ones should be added, replaced, or skipped.</param>
    public static string Combine(HttpRequest request, string path, params Parameter[] parameters)
    {
      StringBuilder result = new StringBuilder();

      foreach (Parameter parameter in parameters)
      {
        if (!parameter.Skip)
        {
          string value = parameter.TakeFromUrl ? request.Query[parameter.Name].ToString() : parameter.Value;

          if (!string.IsNullOrEmpty(value))
            result.AppendFormat("{0}{1}={2}", result.Length == 0 ? '?' : '&', parameter.Name, value);
        }
      }

      foreach (string key in request.Query.Keys)
      {
        if (!parameters.Any(p => p.Name == key))
        {
          string value = request.Query[key];

          if (!string.IsNullOrEmpty(value))
            result.AppendFormat("{0}{1}={2}", result.Length == 0 ? '?' : '&', key, value);
        }
      }

      result.Insert(0, string.IsNullOrEmpty(path) ? request.Path.ToString() : path);
      return result.ToString();
    }
  }
}