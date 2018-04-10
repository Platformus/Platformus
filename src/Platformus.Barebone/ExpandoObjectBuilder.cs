// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Dynamic;

namespace Platformus.Barebone
{
  public class ExpandoObjectBuilder
  {
    private ExpandoObject expandoObject;

    public ExpandoObjectBuilder()
    {
      this.expandoObject = new ExpandoObject();
    }

    public ExpandoObjectBuilder(ExpandoObject expandoObject)
    {
      this.expandoObject = expandoObject;
    }

    public ExpandoObjectBuilder AddProperty(string key, dynamic value)
    {
      (this.expandoObject as IDictionary<string, dynamic>).Add(key, value);
      return this;
    }

    public dynamic Build()
    {
      return this.expandoObject;
    }
  }
}