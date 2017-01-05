// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;

namespace Platformus
{
  public static class LongExtensions
  {
    public static DateTime ToDateTime(this long value)
    {
      DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

      dateTime = dateTime.AddSeconds(value);
      return dateTime.ToLocalTime();
    }
  }
}