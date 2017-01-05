// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Platformus
{
  public class Url
  {
    public class Descriptor
    {
      public string Name { get; set; }
      public string Value { get; set; }
      public bool TakeFromUrl { get; set; }
      public bool Skip { get; set; }

      public Descriptor(string name = null, object value = null, bool takeFromUrl = false, bool skip = false)
      {
        this.Name = name;
        this.Value = value == null ? null : value.ToString();
        this.TakeFromUrl = takeFromUrl;
        this.Skip = skip;
      }
    }

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