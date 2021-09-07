// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  public class TextFilterTagHelper : TagHelper
  {
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }
    public string PropertyPath { get; set; }
    public string Label { get; set; }
    public Size Width { get; set; } = Size.Large;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      TagBuilder tb = TextBoxGenerator.Generate(
        string.Empty,
        InputTypes.Text
      );

      tb.AddCssClass("filter__criterion");

      if (this.Width == Size.Small)
        tb.AddCssClass("filter__criterion--small");

      else if (this.Width == Size.Medium)
        tb.AddCssClass("filter__criterion--medium");

      else if (this.Width == Size.Large)
        tb.AddCssClass("filter__criterion--large");

      tb.MergeAttribute("data-property-path", this.PropertyPath?.ToLower());
      tb.MergeAttribute(AttributeNames.Placeholder, this.Label);
      tb.MergeAttribute(AttributeNames.Value, this.ViewContext.HttpContext.Request.Query[this.PropertyPath]);
      output.SuppressOutput();
      output.Content.AppendHtml(tb);
    }
  }
}