// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend;

public class DecimalBoxTagHelper : TagHelperBase<decimal>
{
  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    if (this.For == null && string.IsNullOrEmpty(this.Id))
      return;

    output.SuppressOutput();
    output.Content.AppendHtml(this.CreateNumericBox());
  }

  private TagBuilder CreateNumericBox()
  {
    TagBuilder tb = NumericBoxGenerator.Generate(
      this.GetIdentity(),
      NumericTypes.Decimal,
      value: this.GetValue(),
      validation: this.GetValidation()
    );

    if (this.IsDisabled())
      tb.MergeAttribute(AttributeNames.Disabled, "disabled");

    // TODO: merge all the attributes, not only "onchange"
    if (!string.IsNullOrEmpty(this.OnChange))
      tb.MergeAttribute(AttributeNames.OnChange, this.OnChange);

    return tb;
  }
}