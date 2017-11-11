// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Reflection;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain
{
  public class StronglyTypedObjectBuilder<T> : ObjectBuilderBase
  {
    private T result;

    public StronglyTypedObjectBuilder()
    {
      this.result = System.Activator.CreateInstance<T>();
    }

    public override void BuildId(int id)
    {
      this.SetPropertyValue("Id", id);
    }

    public override void BuildIntegerProperty(string memberCode, int? value)
    {
      this.SetPropertyValue(memberCode, value);
    }

    public override void BuildDecimalProperty(string memberCode, decimal? value)
    {
      this.SetPropertyValue(memberCode, value);
    }

    public override void BuildStringProperty(string memberCode, string value)
    {
      this.SetPropertyValue(memberCode, value);
    }

    public override void BuildStringProperty(string memberCode, IDictionary<string, string> value)
    {
      this.SetPropertyValue(memberCode, value);
    }

    public override void BuildDateTimeProperty(string memberCode, System.DateTime? value)
    {
      this.SetPropertyValue(memberCode, value);
    }

    public T Build()
    {
      return this.result;
    }

    private void SetPropertyValue(string name, object value)
    {
      PropertyInfo propertyInfo = result.GetType().GetProperty(name);

      if (propertyInfo == null)
        return;

      if (value != null && !propertyInfo.PropertyType.IsAssignableFrom(value.GetType()))
        return;

      if (value == null && System.Nullable.GetUnderlyingType(propertyInfo.PropertyType) == null)
        return;

      propertyInfo.SetValue(this.result, value);
    }
  }
}