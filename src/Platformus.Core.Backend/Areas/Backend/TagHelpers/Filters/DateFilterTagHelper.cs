// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend;

public class DateFilterTagHelper : CriterionTagHelperBase
{
  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    base.Process(context, output);
    output.Content.AppendHtml(this.CreateTextBox());
  }

  protected override string GetValue()
  {
    if (DateTime.TryParse(base.GetValue(), out DateTime value))
      return value.ToFixedLengthDateString();

    return null;
  }

  private TagBuilder CreateTextBox()
  {
    TagBuilder tb = TextBoxGenerator.Generate(
      string.Empty,
      InputTypes.Text,
      value: this.GetValue()
    );

    tb.AddCssClass("filter__criterion filter__criterion--small");
    tb.MergeAttribute(AttributeNames.DataType, "date");
    tb.MergeAttribute("data-property-path", this.PropertyPath?.ToLower());
    return tb;
  }
}