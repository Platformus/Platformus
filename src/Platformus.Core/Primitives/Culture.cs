﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core.Primitives
{
  public class Culture
  {
    public string Code { get; set; }

    public Culture(string code)
    {
      this.Code = code;
    }
  }
}