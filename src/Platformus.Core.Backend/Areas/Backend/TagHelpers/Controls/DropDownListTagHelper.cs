// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend
{
  public class DropDownListTagHelper : TagHelper
  {
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }
    public string Class { get; set; }
    public ModelExpression For { get; set; }
    public IEnumerable<Option> Options { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.For == null || this.Options == null)
        return;

      TagBuilder tb = DropDownListGenerator.Generate(
        this.For.GetIdentity(),
        this.Options,
        this.For.GetValue(this.ViewContext)?.ToString()?.ToString(),
        this.For.HasRequiredAttribute(),
        this.For.IsValid(this.ViewContext)
      );

      if (!string.IsNullOrEmpty(this.Class))
        tb.AddCssClass(this.Class);

      output.SuppressOutput();
      output.Content.AppendHtml(tb);
    }
  }
}