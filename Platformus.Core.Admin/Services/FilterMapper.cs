// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Reflection;
using Magicalizer.Filters.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Platformus.Core.Admin.Services.Abstractions;

namespace Platformus.Core.Admin.Services;

public class FilterMapper : IFilterMapper
{
  private readonly NavigationManager navigationManager;

  public FilterMapper(NavigationManager navigationManager)
  {
    this.navigationManager = navigationManager;
  }

  public T Map<T>() where T : class, IFilter, new()
  {
    T filter = new T();
    Uri url = new Uri(this.navigationManager.Uri);

    foreach (KeyValuePair<string, StringValues> kvp in QueryHelpers.ParseQuery(url.Query))
      SetFilterPropertyValue(filter, kvp.Key.Split('.'), kvp.Value.ToString());

    return filter;
  }

  private static void SetFilterPropertyValue(IFilter filter, string[] propertyPath, string value)
  {
    if (propertyPath.Length == 0) return;

    string propertyName = propertyPath.First();

    foreach (PropertyInfo property in filter.GetType().GetProperties())
    {
      if (!string.Equals(property.Name, propertyName, StringComparison.OrdinalIgnoreCase)) continue;

      if (IsFilter(property))
      {
        IFilter? nestedFilter = Activator.CreateInstance(property.PropertyType) as IFilter;

        if (nestedFilter != null)
        {
          property.SetValue(filter, nestedFilter);
          SetFilterPropertyValue(nestedFilter, propertyPath.Skip(1).ToArray(), value);
        }

        return;
      }

      Type type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

      if (type == typeof(bool))
      {
        if (bool.TryParse(value, out bool result))
          property.SetValue(filter, result);
      }

      else if (type == typeof(byte))
      {
        if (byte.TryParse(value, out byte result))
          property.SetValue(filter, result);
      }

      else if (type == typeof(short))
      {
        if (short.TryParse(value, out short result))
          property.SetValue(filter, result);
      }

      else if (type == typeof(int))
      {
        if (int.TryParse(value, out int result))
          property.SetValue(filter, result);
      }

      else if (type == typeof(long))
      {
        if (long.TryParse(value, out long result))
          property.SetValue(filter, result);
      }

      else if (type == typeof(float))
      {
        if (float.TryParse(value, out float result))
          property.SetValue(filter, result);
      }

      else if (type == typeof(double))
      {
        if (double.TryParse(value, out double result))
          property.SetValue(filter, result);
      }

      else if (type == typeof(decimal))
      {
        if (decimal.TryParse(value, out decimal result))
          property.SetValue(filter, result);
      }

      else if (type == typeof(Guid))
      {
        if (Guid.TryParse(value, out Guid result))
          property.SetValue(filter, result);
      }

      else if (type == typeof(DateTime))
      {
        if (DateTime.TryParse(value, out DateTime result))
          property.SetValue(filter, result);
      }

      else if (type == typeof(string))
        property.SetValue(filter, value);
    }
  }

  private static bool IsFilter(PropertyInfo property)
  {
    return typeof(IFilter).IsAssignableFrom(property.PropertyType);
  }
}
