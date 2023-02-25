// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend;

[RestrictChildren("neutral-button", "positive-button", "negative-button", "delete-button", "partial")]
public class NodeControlsTagHelper : TagHelper
{
  public string Class { get; set; }

  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    output.TagName = TagNames.Div;
    output.TagMode = TagMode.StartTagAndEndTag;

    if (context.Items.ContainsKey("IsNodeHeader") && (bool)context.Items["IsNodeHeader"])
      output.Attributes.SetAttribute(AttributeNames.Class, "node__buttons buttons node__controls" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));

    else output.Attributes.SetAttribute(AttributeNames.Class, "node__buttons buttons" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));
  }
}