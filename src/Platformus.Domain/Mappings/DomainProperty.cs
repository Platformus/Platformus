// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Globalization;

namespace Platformus.Domain
{
  public class DomainProperty : Dictionary<string, string>
  {
    public bool IsEmpty
    {
      get
      {
        return this.Keys.Count == 0;
      }
    }

    public string Value
    {
      get
      {
        string currentCultureCode = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

        if (this.ContainsKey(currentCultureCode))
          return this[currentCultureCode];

        if (this.ContainsKey("__"))
          return this["__"];

        return null;
      }
    }
  }
}