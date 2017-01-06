// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Dynamic;
using Platformus.Domain.Data.Models;
using Platformus.Globalization.Data.Models;

namespace Platformus.Domain
{
  public class DynamicObjectBuilder : ObjectBuilderBase
  {
    private ExpandoObject dynamicObject;

    public DynamicObjectBuilder()
    {
      this.dynamicObject = new ExpandoObject();
    }

    public override void BuildBasics(Object @object)
    {
      (this.dynamicObject as IDictionary<string, dynamic>).Add("Id", @object.Id);
      (this.dynamicObject as IDictionary<string, dynamic>).Add("Url", @object.Url);
      (this.dynamicObject as IDictionary<string, dynamic>).Add("ViewName", @object.ViewName);
    }

    public override void BuildProperty(Object @object, Member member, Property property, IDictionary<Culture, Localization> localizationsByCultures)
    {
      ExpandoObject dynamicProperty = new ExpandoObject();

      foreach (Culture culture in localizationsByCultures.Keys)
        (dynamicProperty as IDictionary<string, dynamic>).Add(culture.Code, localizationsByCultures[culture]?.Value);

      (this.dynamicObject as IDictionary<string, dynamic>).Add(member.Code, dynamicProperty);
    }

    public dynamic Build()
    {
      return this.dynamicObject;
    }
  }
}