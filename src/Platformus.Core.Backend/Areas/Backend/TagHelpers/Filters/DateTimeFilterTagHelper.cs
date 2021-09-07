// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  public class DateTimeFilterTagHelper : TagHelper
  {
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }
    public string PropertyPath { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      TagBuilder tb = TextBoxGenerator.Generate(
        string.Empty,
        InputTypes.Text
      );

      tb.AddCssClass("filter__criterion filter__criterion--medium");
      tb.MergeAttribute(AttributeNames.DataType, "date-time");
      tb.MergeAttribute("data-property-path", this.PropertyPath?.ToLower());
      tb.MergeAttribute(AttributeNames.Value, this.GetValue());
      output.SuppressOutput();
      output.Content.AppendHtml(tb);
    }

    private string GetValue()
    {
      if (DateTime.TryParse(this.ViewContext.HttpContext.Request.Query[this.PropertyPath], out DateTime value))
        return value.ToFixedLengthDateTimeString();

      return null;
    }
  }
}