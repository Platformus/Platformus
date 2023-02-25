// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Website.Data.Entities;

namespace Platformus.Website;

/// <summary>
/// Contains the extension methods of the <see cref="Property"/>.
/// </summary>
public static class PropertyExtensions
{
  /// <summary>
  /// Gets a property value according to its type.
  /// </summary>
  /// <param name="property">A property to get the value of.</param>
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