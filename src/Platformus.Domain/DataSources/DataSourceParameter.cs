// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Domain.DataSources
{
  public class DataSourceParameter
  {
    public string Code { get; set; }
    public string Name { get; set; }
    public string JavaScriptEditorClassName { get; set; }

    public DataSourceParameter(string code, string name, string javaScriptEditorClassName)
    {
      this.Code = code;
      this.Name = name;
      this.JavaScriptEditorClassName = javaScriptEditorClassName;
    }
  }
}