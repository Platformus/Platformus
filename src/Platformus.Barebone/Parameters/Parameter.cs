// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Platformus.Barebone.Primitives;

namespace Platformus.Barebone.Parameters
{
  public class Parameter
  {
    public string Code { get; set; }
    public string Name { get; set; }
    public string JavaScriptEditorClassName { get; set; }
    public IEnumerable<Option> Options { get; set; }
    public string DefaultValue { get; set; }
    public bool IsRequired { get; set; }

    public Parameter(string code, string name, string javaScriptEditorClassName, string defaultValue = null, bool isRequired = false)
    {
      this.Code = code;
      this.Name = name;
      this.JavaScriptEditorClassName = javaScriptEditorClassName;
      this.DefaultValue = defaultValue;
      this.IsRequired = isRequired;
    }

    public Parameter(string code, string name, IEnumerable<Option> options, string javaScriptEditorClassName, string defaultValue = null, bool isRequired = false)
      : this(code, name, javaScriptEditorClassName, defaultValue, isRequired)
    {
      this.Options = options;
    }
  }
}