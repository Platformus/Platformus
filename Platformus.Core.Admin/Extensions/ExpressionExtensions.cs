// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace Platformus.Core.Admin.Extensions;

public static class ExpressionExtensions
{
  public static string? GetDisplayName<TValue>(this Expression<Func<TValue>>? valueExpression)
  {
    if (valueExpression != null && valueExpression.Body is MemberExpression memberExpression)
      return (memberExpression.Member as PropertyInfo)?.GetCustomAttribute<DisplayAttribute>()?.GetName();

    return null;
  }
}