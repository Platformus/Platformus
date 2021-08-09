// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  public class MultilineTextFieldTagHelper : TagHelper
  {
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }
    public string Class { get; set; }
    public ModelExpression For { get; set; }
    public Size Height { get; set; } = Size.Large;
    public bool IsDisabled { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.For == null)
        return;

      output.TagMode = TagMode.StartTagAndEndTag;
      output.TagName = TagNames.Div;
      output.Attributes.SetAttribute(AttributeNames.Class, "form__field field" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));
      output.Content.AppendHtml(this.CreateLabel());
      output.Content.AppendHtml(this.CreateTextArea());
      output.Content.AppendHtml(this.CreateValidationErrorMessage());
    }

    private TagBuilder CreateLabel()
    {
      return FieldGenerator.GenerateLabel(this.For.GetLabel(), this.For.GetIdentity());
    }

    private TagBuilder CreateTextArea()
    {
      TagBuilder tb = TextAreaGenerator.Generate(
        this.For.GetIdentity(),
        this.For.GetValue(this.ViewContext),
        this.For.HasRequiredAttribute(),
        this.For.HasStringLengthAttribute() ? this.For.GetMaxStringLength() : null,
        this.For.IsValid(this.ViewContext)
      );

      tb.AddCssClass("field__text-area");

      if (this.Height == Size.Medium)
        tb.AddCssClass("field__text-area--medium");

      else if (this.Height == Size.Small)
        tb.AddCssClass("field__text-area--small");

      if (this.IsDisabled)
        tb.MergeAttribute("disabled", "disabled");

      return tb;
    }

    private TagBuilder CreateValidationErrorMessage()
    {
      return ValidationErrorMessageGenerator.Generate(this.For.GetIdentity());
    }
  }
}