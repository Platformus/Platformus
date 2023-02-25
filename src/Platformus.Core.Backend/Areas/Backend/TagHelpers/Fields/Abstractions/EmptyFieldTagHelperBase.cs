// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend;

public abstract class EmptyFieldTagHelperBase<T> : TagHelperBase<T>
{
  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    if (this.For == null && string.IsNullOrEmpty(this.Id))
      return;

    output.TagMode = TagMode.StartTagAndEndTag;
    output.TagName = TagNames.Div;
    output.Attributes.SetAttribute(AttributeNames.Class, "form__field field" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));
  }
}