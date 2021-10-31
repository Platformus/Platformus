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
    public class Descriptor
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
      /// Initializes a new instance of the <see cref="Descriptor"/> class.
      /// </summary>
      /// <param name="name">The parameter's name.</param>
      /// <param name="value">The parameter's value (if parameter should be added or replaced).</param>
      /// <param name="takeFromUrl">A value indicating that the parameter should be copied from the current HTTP(S) request's query string.</param>
      /// <param name="skip">A value indicating that the parameter should be skipped from the current HTTP(S) request's query string.</param>
      public Descriptor(string name = null, object value = null, bool takeFromUrl = false, bool skip = false)
      {
        this.Name = name;
        this.Value = value == null ? null : value.ToString();
        this.TakeFromUrl = takeFromUrl;
        this.Skip = skip;
      }
    }

    /// <summary>
    /// Combines a new URL using the given path and current HTTP(S) request's query string
    /// according to the provided descriptors.
    /// </summary>
    /// <param name="request">The current HTTP(S) request.</param>
    /// <param name="path">The resulting URL's path (example: '/some/new/path').</param>
    /// <param name="descriptors">The descriptors specifying which parameters should be copied from the current request,
    /// which ones should be added, replaced, or skipped.</param>
    public static string Combine(HttpRequest request, string path, params Descriptor[] descriptors)
    {
      StringBuilder result = new StringBuilder();

      foreach (Descriptor descriptor in descriptors)
      {
        if (!descriptor.Skip)
        {
          string value = descriptor.TakeFromUrl ? request.Query[descriptor.Name].ToString() : descriptor.Value;

          if (!string.IsNullOrEmpty(value))
            result.AppendFormat("{0}{1}={2}", result.Length == 0 ? '?' : '&', descriptor.Name, value);
        }
      }

      foreach (string key in request.Query.Keys)
      {
        if (!descriptors.Any(d => d.Name == key))
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