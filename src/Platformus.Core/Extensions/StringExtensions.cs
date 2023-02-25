// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Platformus;

/// <summary>
/// Contains the extension methods of the <see cref="string"/>.
/// </summary>
public static class StringExtensions
{
  /// <summary>
  /// Converts a given string into a camel case. Example: "SomeValue" => "someValue".
  /// </summary>
  /// <param name="value">A string to convert.</param>
  public static string ToCamelCase(this string value)
  {
    if (string.IsNullOrEmpty(value))
      return value;

    if (value.Length == 1)
      return value.ToLower();

    return value.Remove(1).ToLower() + value.Substring(1);
  }

  /// <summary>
  /// Parses a string into an <see cref="int"/> value. If parsing fails, the <paramref name="defaultValue"/> is returned instead.
  /// </summary>
  /// <param name="value">A string to parse.</param>
  /// <param name="defaultValue">A default value to use if parsing fails.</param>
  public static int ToIntWithDefaultValue(this string value, int defaultValue)
  {
    if (int.TryParse(value, out int result))
      return result;

    return defaultValue;
  }

  /// <summary>
  /// Parses a string into an <see cref="decimal"/> value. If parsing fails, the <paramref name="defaultValue"/> is returned instead.
  /// </summary>
  /// <param name="value">A string to parse.</param>
  /// <param name="defaultValue">A default value to use if parsing fails.</param>
  public static decimal ToDecimalWithDefaultValue(this string value, decimal defaultValue)
  {
    if (decimal.TryParse(value, out decimal result))
      return result;

    return defaultValue;
  }

  /// <summary>
  /// Parses a string into an <see cref="bool"/> value. If parsing fails the <paramref name="defaultValue"/> is returned instead.
  /// </summary>
  /// <param name="value">A string to parse.</param>
  /// <param name="defaultValue">A default value to use if parsing fails.</param>
  public static bool ToBoolWithDefaultValue(this string value, bool defaultValue)
  {
    if (bool.TryParse(value, out bool result))
      return result;

    return defaultValue;
  }

  /// <summary>
  /// Parses a string into an <see cref="DateTime"/> value. If parsing fails the <paramref name="defaultValue"/> is returned instead.
  /// </summary>
  /// <param name="value">A string to parse.</param>
  /// <param name="defaultValue">A default value to use if parsing fails.</param>
  public static DateTime ToDateTimeWithDefaultValue(this string value, DateTime defaultValue)
  {
    if (DateTime.TryParse(value, out DateTime result))
      return result;

    return defaultValue;
  }
}