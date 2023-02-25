// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend;

public class CheckboxFieldTagHelper : EmptyFieldTagHelperBase<bool>
{
  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    if (this.For == null && string.IsNullOrEmpty(this.Id))
      return;

    base.Process(context, output);
    output.Content.AppendHtml(this.CreateCheckbox());
  }

  private TagBuilder CreateCheckbox()
  {
    bool.TryParse(this.GetValue(), out bool value);

    TagBuilder tb = CheckboxGenerator.Generate(
      this.GetIdentity(),
      this.GetLabel(),
      value: value
    );

    // TODO: merge all the attributes, not only "onchange"
    if (!string.IsNullOrEmpty(this.OnChange))
      tb.MergeAttribute(AttributeNames.OnChange, this.OnChange);

    return tb;
  }
}