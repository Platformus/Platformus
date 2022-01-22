// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  public class DateFieldTagHelper : FieldTagHelperBase<DateTime?>
  {
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.For == null && string.IsNullOrEmpty(this.Id))
        return;

      base.Process(context, output);
      output.Content.AppendHtml(this.CreateTextBox());
      output.Content.AppendHtml(this.CreateValidationErrorMessage());
    }

    protected override string FormatValue(object value)
    {
      return value == null ? null : ((DateTime)value).ToFixedLengthDateString();
    }

    private TagBuilder CreateTextBox()
    {
      TagBuilder tb = TextBoxGenerator.Generate(
        this.GetIdentity(),
        InputTypes.Text,
        value: this.GetValue(),
        validation: this.GetValidation()
      );

      tb.AddCssClass("field__text-box");

      if (this.Disabled)
        tb.MergeAttribute(AttributeNames.Disabled, "disabled");

      tb.MergeAttribute(AttributeNames.DataType, "date");

      // TODO: merge all the attributes, not only "onchange"
      if (!string.IsNullOrEmpty(this.OnChange))
        tb.MergeAttribute(AttributeNames.OnChange, this.OnChange);

      return tb;
    }
  }
}