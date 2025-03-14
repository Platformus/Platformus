// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Data;
using System.Reflection;

namespace Platformus.Website.Domain.Services.Extensions;

public static class IEnumerableExtensions
{
  public static DataTable ToDataTable<T>(this IEnumerable<T> entities)
  {
    DataTable result = new DataTable();
    PropertyInfo[] properties = typeof(T).GetProperties().Where(p => {
      MethodInfo? method = p.GetGetMethod();

      return method != null && (!method.IsVirtual || method.IsFinal);
    }).ToArray();

    foreach (PropertyInfo property in properties)
    {
      Type? propertyType = property.PropertyType;

      if (propertyType.IsEnum)
      {
        int @int = 0;
        propertyType = @int.GetType();
      }

      if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
        propertyType = Nullable.GetUnderlyingType(propertyType);

      if (propertyType != null)
        result.Columns.Add(property.Name, propertyType);
    }

    object?[] values = new object[properties.Length];

    foreach (T entity in entities)
    {
      for (int i = 0; i != properties.Length; i++)
        values[i] = properties[i].GetValue(entity);

      result.Rows.Add(values);
    }

    return result;
  }
}
