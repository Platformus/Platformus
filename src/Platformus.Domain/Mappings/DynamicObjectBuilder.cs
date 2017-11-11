// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Dynamic;
using Platformus.Domain.Data.Entities;

namespace Platformus.Domain
{
  public class DynamicObjectBuilder : ObjectBuilderBase
  {
    private ExpandoObject result;

    public DynamicObjectBuilder()
    {
      this.result = new ExpandoObject();
    }

    public override void BuildId(int id)
    {
      (this.result as IDictionary<string, dynamic>).Add("Id", id);
    }

    public override void BuildIntegerProperty(string memberCode, int? value)
    {
      (this.result as IDictionary<string, dynamic>).Add(memberCode, value);
    }

    public override void BuildDecimalProperty(string memberCode, decimal? value)
    {
      (this.result as IDictionary<string, dynamic>).Add(memberCode, value);
    }

    public override void BuildStringProperty(string memberCode, string value)
    {
      (this.result as IDictionary<string, dynamic>).Add(memberCode, value);
    }

    public override void BuildStringProperty(string memberCode, IDictionary<string, string> value)
    {
      (this.result as IDictionary<string, dynamic>).Add(memberCode, value);
    }

    public override void BuildDateTimeProperty(string memberCode, System.DateTime? value)
    {
      (this.result as IDictionary<string, dynamic>).Add(memberCode, value);
    }

    public dynamic Build()
    {
      return this.result;
    }
  }
}