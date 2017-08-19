// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace Platformus.Forms.FormHandlers
{
  public class FormHandlerParameterGroup
  {
    public string Name { get; set; }
    public IEnumerable<FormHandlerParameter> Parameters { get; set; }

    public FormHandlerParameterGroup(string name, params FormHandlerParameter[] parameters)
    {
      this.Name = name;
      this.Parameters = parameters;
    }
  }
}