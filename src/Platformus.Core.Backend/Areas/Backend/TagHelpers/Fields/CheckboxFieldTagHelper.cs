// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  public class CheckboxFieldTagHelper : TagHelper
  {
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }
    public string Class { get; set; }
    public ModelExpression For { get; set; }
    public string Id { get; set; }
    public string Label { get; set; }
    public bool IsChecked { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.For == null && string.IsNullOrEmpty(this.Id))
        return;

      output.TagMode = TagMode.StartTagAndEndTag;
      output.TagName = TagNames.Div;
      output.Attributes.SetAttribute(AttributeNames.Class, "form__field field" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));
      output.Content.AppendHtml(this.CreateCheckbox());
    }

    private TagBuilder CreateCheckbox()
    {
      if (this.For != null)
      {
        bool isChecked = string.Equals(this.For.GetValue(this.ViewContext), true.ToString(), StringComparison.OrdinalIgnoreCase);

        return CheckboxGenerator.Generate(
          this.For.GetIdentity(), this.For.GetLabel(), isChecked
        );
      }

      return CheckboxGenerator.Generate(
        this.Id, this.Label, this.IsChecked
      );
    }
  }
}