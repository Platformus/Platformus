// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Domain.Data.Models;
using Platformus.Globalization.Data.Models;

namespace Platformus.Domain
{
  public abstract class ObjectBuilderBase
  {
    public abstract void BuildBasics(Object @object);
    public abstract void BuildProperty(Object @object, Member member, Property property, IDictionary<Culture, Localization> localizationsByCultures);
  }
}