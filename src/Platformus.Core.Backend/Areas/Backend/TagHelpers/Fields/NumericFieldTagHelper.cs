// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

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
    public bool IsDisabled { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.For == null)
        return;

      output.TagMode = TagMode.StartTagAndEndTag;
      output.TagName = TagNames.Div;
      output.Attributes.SetAttribute(AttributeNames.Class, "form__field field" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));
      output.Content.AppendHtml(this.CreateLabel());
      output.Content.AppendHtml(this.CreateTextBox());
    }

    private TagBuilder CreateLabel()
    {
      return FieldGenerator.GenerateLabel(this.For.GetLabel(), this.For.GetIdentity());
    }

    private TagBuilder CreateTextBox()
    {
      TagBuilder tb = TextBoxGenerator.Generate(
        this.For.GetIdentity(),
        InputTypes.Text,
        this.For.GetValue(this.ViewContext)?.ToString(),
        this.For.HasRequiredAttribute(),
        this.For.HasStringLengthAttribute() ? this.For.GetMaxStringLength() : null,
        this.For.IsValid(this.ViewContext)
      ); ;

      tb.AddCssClass("field__text-box field__text-box--numeric");

      if (this.IsDisabled)
        tb.MergeAttribute(AttributeNames.Disabled, "disabled");

      tb.MergeAttribute(AttributeNames.DataType, "number");
      return tb;
    }
  }
}