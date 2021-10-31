﻿// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  public class DateTimeFieldTagHelper : TagHelper
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
      output.Content.AppendHtml(this.CreateValidationErrorMessage());
    }

    private TagBuilder CreateLabel()
    {
      return FieldGenerator.GenerateLabel(this.For.GetLabel(), this.For.GetIdentity());
    }

    private TagBuilder CreateTextBox()
    {
      object value = this.For.GetValue(this.ViewContext);
      TagBuilder tb = TextBoxGenerator.Generate(
        this.For.GetIdentity(),
        InputTypes.Text,
        value == null ? null : ((DateTime)value).ToFixedLengthDateTimeString(),
        this.For.HasRequiredAttribute(),
        this.For.HasStringLengthAttribute() ? this.For.GetMaxStringLength() : null,
        this.For.IsValid(this.ViewContext)
      );

      tb.AddCssClass("field__text-box");

      if (this.IsDisabled)
        tb.MergeAttribute(AttributeNames.Disabled, "disabled");

      tb.MergeAttribute(AttributeNames.DataType, "date-time");
      return tb;
    }

    private TagBuilder CreateValidationErrorMessage()
    {
      return ValidationErrorMessageGenerator.Generate(this.For.GetIdentity());
    }
  }
}