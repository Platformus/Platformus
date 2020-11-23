// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Platformus.Core;
using Platformus.Website.Data.Entities;

namespace Platformus.Website
{
  public static class ObjectExtensions
  {
    public static IEnumerable<object> GetVisibleInListPropertyValues(this Object @object, HttpContext httpContext)
    {
      return @object.Class.GetVisibleInListMembers().Select(
        m => @object.Properties.FirstOrDefault(p => p.MemberId == m.Id)?.GetValue(httpContext)
      );
    }

    public static dynamic ToDisplayable(this Object @object, HttpContext httpContext)
    {
      ExpandoObjectBuilder expandoObjectBuilder = new ExpandoObjectBuilder();

      foreach (Property property in @object.Properties)
        if (property.Member.IsPropertyVisibleInList == true)
          expandoObjectBuilder.AddProperty(property.Member.Code, property.GetValue(httpContext));

      return expandoObjectBuilder.Build();
    }
  }
}