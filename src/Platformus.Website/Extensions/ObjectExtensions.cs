// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Platformus.Website.Data.Entities;

namespace Platformus.Website;

/// <summary>
/// Contains the extension methods of the <see cref="Object"/>.
/// </summary>
public static class ObjectExtensions
{
  /// <summary>
  /// Gets the object's properties that should be visible in list.
  /// "Visible in list" means that the member's value should be displayed in the object list in the backend.
  /// </summary>
  /// <param name="object">An object to get properties of.</param>
  public static IEnumerable<object> GetVisibleInListPropertyValues(this Object @object)
  {
    return @object.Class.GetVisibleInListMembers().Select(
      m => @object.Properties.FirstOrDefault(p => p.MemberId == m.Id)?.GetValue()
    );
  }

  /// <summary>
  /// Creates dynamic object that contains only visible in list properties from the given one.
  /// </summary>
  /// <param name="object">An object to get properties of.</param>
  public static dynamic ToDisplayable(this Object @object)
  {
    ExpandoObjectBuilder expandoObjectBuilder = new ExpandoObjectBuilder();

    foreach (Property property in @object.Properties)
      if (property.Member.IsPropertyVisibleInList == true)
        expandoObjectBuilder.AddProperty(property.Member.Code, property.GetValue());

    return expandoObjectBuilder.Build();
  }
}