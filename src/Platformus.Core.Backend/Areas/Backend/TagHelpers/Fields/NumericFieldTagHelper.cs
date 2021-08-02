// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  public class NumericFieldTagHelper : TagHelper
  {
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }
    public string Class { get; set; }
    public ModelExpression For { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.For == null)
        return;

      output.SuppressOutput();
      output.Content.AppendHtml(this.GenerateField(output.Attributes));
    }

    private TagBuilder GenerateField(TagHelperAttributeList attributes)
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("form__field field" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));
      tb.InnerHtml.AppendHtml(FieldGenerator.GenerateLabel(this.For.GetLabel(), this.For.GetIdentity()));

      TagBuilder tbTextBox = TextBoxGenerator.Generate(
        this.For.GetIdentity(), "text", this.For.GetValue(this.ViewContext)
      );

      tbTextBox.AddCssClass("field__text-box field__text-box--numeric");
      tb.InnerHtml.AppendHtml(tbTextBox);
      tb.InnerHtml.AppendHtml(this.GenerateNumericButtons());
      tb.InnerHtml.AppendHtml(ValidationErrorMessageGenerator.Generate(this.For.GetIdentity()));
      return tb;
    }

    private IHtmlContent GenerateNumericButtons()
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("field__numeric-buttons");
      tb.InnerHtml.AppendHtml(this.GenerateNumericButtonUp());
      tb.InnerHtml.AppendHtml(this.GenerateNumericButtonDown());
      return tb;
    }

    private IHtmlContent GenerateNumericButtonUp()
    {
      TagBuilder tb = new TagBuilder(TagNames.A);

      tb.AddCssClass("field__numeric-button field__numeric-button--up");
      return tb;
    }

    private IHtmlContent GenerateNumericButtonDown()
    {
      TagBuilder tb = new TagBuilder(TagNames.A);

      tb.AddCssClass("field__numeric-button field__numeric-button--down");
      return tb;
    }
  }
}