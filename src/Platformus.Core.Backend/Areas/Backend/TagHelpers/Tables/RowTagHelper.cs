// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  [RestrictChildren("cell", "image-cell", "buttons-cell", "partial")]
  public class RowTagHelper : TagHelper
  {
    public string Class { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      output.TagName = TagNames.TR;
      output.Attributes.SetAttribute(AttributeNames.Class, "table__row" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));
    }
  }
}