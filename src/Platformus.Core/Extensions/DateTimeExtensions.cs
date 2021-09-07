// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;

namespace Platformus
{
  public static class DateTimeExtensions
  {
    public static string ToFixedLengthDateString(this DateTime value)
    {
      string format = CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;

      if (!format.Contains("MM"))
        format = format.Replace("M", "MM");

      if (!format.Contains("dd"))
        format = format.Replace("d", "dd");

      return value.ToString(format);
    }

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