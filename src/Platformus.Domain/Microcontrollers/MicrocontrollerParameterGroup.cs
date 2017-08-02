// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Platformus.Domain
{
  public class MicrocontrollerParameterGroup
  {
    public string Name { get; set; }
    public IEnumerable<MicrocontrollerParameter> MicrocontrollerParameters { get; set; }

    public MicrocontrollerParameterGroup(string name, params MicrocontrollerParameter[] microcontrollerParameters)
    {
      this.Name = name;
      this.MicrocontrollerParameters = microcontrollerParameters;
    }
  }
}