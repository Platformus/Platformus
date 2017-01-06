// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Reflection;
using Platformus.Domain.Data.Models;
using Platformus.Globalization.Data.Models;

namespace Platformus.Domain
{
  public class DomainObjectBuilder<T> : ObjectBuilderBase where T : DomainObject
  {
    private T domainObject;

    public DomainObjectBuilder()
    {
      this.domainObject = System.Activator.CreateInstance<T>();
    }

    public override void BuildBasics(Object @object)
    {
      this.domainObject.Id = @object.Id;
      this.domainObject.Url = @object.Url;
      this.domainObject.ViewName = @object.ViewName;
    }

    public override void BuildProperty(Object @object, Member member, Property property, IDictionary<Culture, Localization> localizationsByCultures)
    {
      PropertyInfo propertyInfo = domainObject.GetType().GetProperty(member.Code);

      if (propertyInfo == null || propertyInfo.PropertyType != typeof(DomainProperty))
        return;

      DomainProperty domainProperty = new DomainProperty();

      foreach (Culture culture in localizationsByCultures.Keys)
        domainProperty.Add(culture.Code, localizationsByCultures[culture]?.Value);

      propertyInfo.SetValue(domainObject, domainProperty);
    }

    public T Build()
    {
      return this.domainObject;
    }
  }
}