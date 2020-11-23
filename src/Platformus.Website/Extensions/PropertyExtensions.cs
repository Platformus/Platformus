// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Platformus.Website.Data.Entities;

namespace Platformus.Website
{
  public static class PropertyExtensions
  {
    public static object GetValue(this Property property, HttpContext httpContext)
    {
      if (property.IntegerValue != null)
        return property.IntegerValue;

      else if (property.DecimalValue != null)
        return property.DecimalValue;

      else if (property.StringValue != null)
      {
        if (property.Member.IsPropertyLocalizable == true)
          return property.StringValue.GetLocalizationValue(httpContext);

        return property.StringValue.GetNeutralLocalizationValue(httpContext);
      }

      else if (property.DateTimeValue != null)
        return property.DateTimeValue;

      return null;
    }
  }
}