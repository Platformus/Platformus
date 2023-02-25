// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend;

public class DropDownListFieldTagHelper : FieldTagHelperBase<string>
{
  public IEnumerable<Option> Options { get; set; }

  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    if ((this.For == null && string.IsNullOrEmpty(this.Id)) || this.Options == null)
      return;

    base.Process(context, output);
    output.Content.AppendHtml(this.CreateDropDownList());
    output.Content.AppendHtml(this.CreateValidationErrorMessage());
  }

  private TagBuilder CreateDropDownList()
  {
    TagBuilder tb = DropDownListGenerator.Generate(
      this.GetIdentity(),
      this.Options,
      value: this.GetValue(),
      validation: this.GetValidation()
    );

    tb.AddCssClass("field__drop-down-list");

    // TODO: merge all the attributes, not only "onchange"
    if (!string.IsNullOrEmpty(this.OnChange))
      tb.MergeAttribute(AttributeNames.OnChange, this.OnChange);

    return tb;
  }
}