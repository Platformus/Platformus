// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Website.Data.Entities;

namespace Platformus.Website
{
  public static class PropertyExtensions
  {
    public static object GetValue(this Property property)
    {
      if (property.IntegerValue != null)
        return property.IntegerValue;

      else if (property.DecimalValue != null)
        return property.DecimalValue;

      else if (property.StringValue != null)
      {
        if (property.Member.IsPropertyLocalizable == true)
          return property.StringValue.GetLocalizationValue();

        return property.StringValue.GetNeutralLocalizationValue();
      }

      else if (property.DateTimeValue != null)
        return property.DateTimeValue;

      return null;
    }
  }
}