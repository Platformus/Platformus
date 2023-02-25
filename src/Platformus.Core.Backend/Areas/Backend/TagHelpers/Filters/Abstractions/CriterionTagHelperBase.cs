// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend;

public abstract class CriterionTagHelperBase : TagHelper
{
  [HtmlAttributeNotBound]
  [ViewContext]
  public ViewContext ViewContext { get; set; }
  public string PropertyPath { get; set; }
  public string Label { get; set; }

  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    output.SuppressOutput();
  }

  protected virtual string GetValue()
  {
    return this.ViewContext.HttpContext.Request.Query[this.PropertyPath];
  }
}