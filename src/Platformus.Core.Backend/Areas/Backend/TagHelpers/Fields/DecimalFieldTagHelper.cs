// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  public class DecimalFieldTagHelper : FieldTagHelperBase<decimal?>
  {
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.For == null && string.IsNullOrEmpty(this.Id))
        return;

      base.Process(context, output);
      output.Content.AppendHtml(this.CreateNumericBox());
      output.Content.AppendHtml(this.CreateValidationErrorMessage());
    }

    private TagBuilder CreateNumericBox()
    {
      TagBuilder tb = NumericBoxGenerator.Generate(
        this.GetIdentity(),
        NumericTypes.Decimal,
        value: this.GetValue(),
        validation: this.GetValidation()
      );

      tb.AddCssClass("field__numeric-box");

      if (this.IsDisabled())
        tb.MergeAttribute(AttributeNames.Disabled, "disabled");

      // TODO: merge all the attributes, not only "onchange"
      if (!string.IsNullOrEmpty(this.OnChange))
        tb.MergeAttribute(AttributeNames.OnChange, this.OnChange);

      return tb;
    }
  }
}