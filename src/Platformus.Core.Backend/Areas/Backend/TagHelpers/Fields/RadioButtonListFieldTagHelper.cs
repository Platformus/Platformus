// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend
{
  public class RadioButtonListFieldTagHelper : FieldTagHelperBase<string>
  {
    public IEnumerable<Option> Options { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if ((this.For == null && string.IsNullOrEmpty(this.Id)) || this.Options == null)
        return;

      base.Process(context, output);
      output.Content.AppendHtml(this.CreateRadioButtonList());
    }

    private TagBuilder CreateRadioButtonList()
    {
      TagBuilder tb = RadioButtonListGenerator.Generate(
        this.GetIdentity(),
        this.Options,
        value: this.GetValue()
      );

      tb.AddCssClass("field__radio-button-list");

      // TODO: merge all the attributes, not only "onchange"
      if (!string.IsNullOrEmpty(this.OnChange))
        tb.MergeAttribute(AttributeNames.OnChange, this.OnChange);

      return tb;
    }
  }
}