// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  public class MultilineTextFieldTagHelper : TextFieldTagHelperBase<string>
  {
    public Sizes Height { get; set; } = Sizes.Large;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.For == null && string.IsNullOrEmpty(this.Id))
        return;

      this.Class += " form__field--unlimited";
      base.Process(context, output);
      output.Content.AppendHtml(this.CreateTextArea());
      output.Content.AppendHtml(this.CreateValidationErrorMessage());
    }

    private TagBuilder CreateTextArea()
    {
      TagBuilder tb = TextAreaGenerator.Generate(
        this.GetIdentity(),
        value: this.GetValue(),
        validation: this.GetValidation()
      );

      tb.AddCssClass("field__text-area");

      if (this.Height == Sizes.Medium)
        tb.AddCssClass("field__text-area--medium");

      else if (this.Height == Sizes.Small)
        tb.AddCssClass("field__text-area--small");

      if (this.IsDisabled())
        tb.MergeAttribute(AttributeNames.Disabled, "disabled");

      // TODO: merge all the attributes, not only "onchange"
      if (!string.IsNullOrEmpty(this.OnChange))
        tb.MergeAttribute(AttributeNames.OnChange, this.OnChange);

      return tb;
    }
  }
}