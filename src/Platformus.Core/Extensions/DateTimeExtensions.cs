// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;

namespace Platformus
{
  /// <summary>
  /// Contains the extension methods of the <see cref="DateTime"/>.
  /// </summary>
  public static class DateTimeExtensions
  {
    /// <summary>
    /// Converts a <see cref="DateTime"/> value into a fixed-length date string.
    /// Example: 1/2/2003 will be converted as 01/02/2003.
    /// </summary>
    /// <param name="value">A <see cref="DateTime"/> value to convert.</param>
    public static string ToFixedLengthDateString(this DateTime value)
    {
      string format = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;

      if (!format.Contains("MM"))
        format = format.Replace("M", "MM");

      if (!format.Contains("dd"))
        format = format.Replace("d", "dd");

      return value.ToString(format);
    }

    /// <summary>
    /// Converts a <see cref="DateTime"/> value into a fixed-length date and time string.
    /// Example: 1/2/2003 4:5 will be converted as 01/02/2003 04:05.
    /// </summary>
    /// <param name="value">A <see cref="DateTime"/> value to convert.</param>
    public static string ToFixedLengthDateTimeString(this DateTime value)
    {
      string format = $"{CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern} {CultureInfo.CurrentUICulture.DateTimeFormat.ShortTimePattern}";

      if (!format.Contains("MM"))
        format = format.Replace("M", "MM");

      if (!format.Contains("dd"))
        format = format.Replace("d", "dd");

      if (!format.Contains("hh"))
        format = format.Replace("h", "hh");

      if (!format.Contains("HH"))
        format = format.Replace("H", "HH");

      return value.ToString(format);
    }
  }
}