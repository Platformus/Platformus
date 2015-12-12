// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Mvc.ViewFeatures;
using Microsoft.AspNet.Razor.TagHelpers;

namespace Platformus.Barebone.Backend
{
  [HtmlTargetElement("drop-down-list-selector", Attributes = ForAttributeName + "," + OptionsAttributeName)]
  public class DropDownListSelectorTagHelper : DropDownListTagHelperBase
  {
    private const string ForAttributeName = "asp-for";
    private const string OptionsAttributeName = "asp-options";

    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }

    [HtmlAttributeName(ForAttributeName)] 
    public ModelExpression For { get; set; }

    [HtmlAttributeName(OptionsAttributeName)]
    public IEnumerable<Option> Options { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.ViewContext == null || this.For == null || this.Options == null)
        return;

      output.SuppressOutput();
      output.Content.Clear();
      output.Content.Append(this.GenerateField());
    }

    private TagBuilder GenerateField()
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("field");
      tb.InnerHtml.Clear();
      tb.InnerHtml.Append(
        new CompositeHtmlContent(
          this.GenerateLabel(this.For),
          this.GenerateDropDownList(this.ViewContext, this.For, this.Options)
        )
      );

      return tb;
    }
  }
}