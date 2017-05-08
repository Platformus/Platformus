// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Domain.Data.Models;

namespace Platformus.Domain
{
  public abstract class ObjectBuilderBase
  {
    public abstract void BuildId(Object @object);
    public abstract void BuildIntegerProperty(string memberCode, int? value);
    public abstract void BuildDecimalProperty(string memberCode, decimal? value);
    public abstract void BuildStringProperty(string memberCode, IDictionary<string, string> value);
    public abstract void BuildDateTimeProperty(string memberCode, System.DateTime? value);
  }
}