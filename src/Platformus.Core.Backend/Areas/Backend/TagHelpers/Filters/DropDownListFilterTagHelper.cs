// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend
{
  public class DropDownListFilterTagHelper : TagHelper
  {
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }
    public string PropertyPath { get; set; }
    public IEnumerable<Option> Options { get; set; }
    public Size Width { get; set; } = Size.Large;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.Options == null)
        return;

      TagBuilder tb = DropDownListGenerator.Generate(
        string.Empty,
        this.Options,
        this.GetValue()
      );

      tb.AddCssClass("filter__criterion");

      if (this.Width == Size.Small)
        tb.AddCssClass("filter__criterion--small");

      else if (this.Width == Size.Medium)
        tb.AddCssClass("filter__criterion--medium");

      else if (this.Width == Size.Large)
        tb.AddCssClass("filter__criterion--large");

      tb.MergeAttribute("data-property-path", this.PropertyPath?.ToLower());
      output.SuppressOutput();
      output.Content.AppendHtml(tb);
    }

    private string GetValue()
    {
      return (this.Options.FirstOrDefault(o => o.Value == this.ViewContext.HttpContext.Request.Query[this.PropertyPath]) ?? this.Options.FirstOrDefault())?.Value;
    }
  }
}