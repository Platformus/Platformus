// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Barebone.Backend
{
  [HtmlTargetElement("numeric-field", Attributes = ForAttributeName)]
  public class NumericFieldTagHelper : TagHelper
  {
    private const string ForAttributeName = "asp-for";

    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }

    [HtmlAttributeName(ForAttributeName)] 
    public ModelExpression For { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.For == null)
        return;

      output.SuppressOutput();
      output.Content.Clear();
      output.Content.AppendHtml(this.GenerateField(output.Attributes));
    }

    private TagBuilder GenerateField(TagHelperAttributeList attributes)
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("form__field field");
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(new FieldGenerator().GenerateLabel(this.For));
      tb.InnerHtml.AppendHtml(new TextBoxGenerator().GenerateTextBox(this.ViewContext, this.For, attributes, null, "text", "field__text-box field__text-box--numeric"));
      tb.InnerHtml.AppendHtml(this.GenerateNumericButtons());
      return tb;
    }

    private IHtmlContent GenerateNumericButtons()
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("field__numeric-buttons");
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(this.GenerateNumericButtonUp());
      tb.InnerHtml.AppendHtml(this.GenerateNumericButtonDown());
      return tb;
    }

    private IHtmlContent GenerateNumericButtonUp()
    {
      TagBuilder tb = new TagBuilder("a");

      tb.AddCssClass("field__numeric-button field__numeric-button--up");
      return tb;
    }

    private IHtmlContent GenerateNumericButtonDown()
    {
      TagBuilder tb = new TagBuilder("a");

      tb.AddCssClass("field__numeric-button field__numeric-button--down");
      return tb;
    }
  }
}