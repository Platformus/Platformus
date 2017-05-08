// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Platformus
{
  public static class StringExtensions
  {
    public static int ToIntWithDefaultValue(this string value, int defaultValue)
    {
      if (int.TryParse(value, out int result))
        return result;

      return defaultValue;
    }

    public static decimal ToDecimalWithDefaultValue(this string value, decimal defaultValue)
    {
      if (decimal.TryParse(value, out decimal result))
        return result;

      return defaultValue;
    }

    public static DateTime ToDateTimeWithDefaultValue(this string value, DateTime defaultValue)
    {
      if (DateTime.TryParse(value, out DateTime result))
        return result;

      return defaultValue;
    }
  }
}