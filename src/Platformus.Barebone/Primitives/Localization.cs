// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Barebone.Primitives
{
  public class Localization
  {
    public Culture Culture { get; set; }
    public string Value { get; set; }

    public Localization(Culture culture, string value)
    {
      this.Culture = culture;
      this.Value = value;
    }
  }
}