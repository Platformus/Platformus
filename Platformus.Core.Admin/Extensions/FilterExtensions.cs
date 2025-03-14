// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Reflection;
using Magicalizer.Filters.Abstractions;

namespace Platformus.Core.Admin.Extensions;

public static class FilterExtensions
{
  public static string ToQueryString(this IFilter filter)
  {
    IList<string> parameters = [];

    BuildQueryString(filter, [], parameters);
    return string.Join('&', parameters);
  }

  private static void BuildQueryString(IFilter filter, string[] propertyPath, IList<string> parameters)
  {
    foreach (PropertyInfo property in filter.GetType().GetProperties())
    {
      if (property.IsDefined(typeof(IgnoreFilterAttribute), inherit: false)) continue;

      object? propertyValue = property.GetValue(filter);

      if (propertyValue == null) continue;

      propertyPath = [..propertyPath, property.Name];

      if (IsValue(property))
        parameters.Add($"{string.Join('.', propertyPath).ToLower()}={Uri.EscapeDataString(propertyValue.ToString() ?? string.Empty)}");

      // TODO: process IEnumerable.

      else if (IsFilter(property))
        BuildQueryString((propertyValue as IFilter)!, propertyPath, parameters);
    }
  }

  private static bool IsValue(PropertyInfo property)
  {
    Type type = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;

    return new Type[] {
      typeof(bool), typeof(byte), typeof(short), typeof(int), typeof(long), typeof(float), typeof(double), typeof(decimal), typeof(string), typeof(Guid), typeof(DateTime),
      typeof(IEnumerable<byte>), typeof(IEnumerable<short>), typeof(IEnumerable<int>), typeof(IEnumerable<long>), typeof(IEnumerable<float>), typeof(IEnumerable<double>), typeof(IEnumerable<decimal>), typeof(IEnumerable<string>), typeof(IEnumerable<Guid>),
    }.Contains(type);
  }

  private static bool IsFilter(PropertyInfo property)
  {
    return typeof(IFilter).IsAssignableFrom(property.PropertyType);
  }
}