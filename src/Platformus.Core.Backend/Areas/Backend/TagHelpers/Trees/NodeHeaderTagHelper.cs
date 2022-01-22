// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  [RestrictChildren("node-title", "node-controls", "partial")]
  public class NodeHeaderTagHelper : TagHelper
  {
    public string Class { get; set; }
    public string Href { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      output.TagName = TagNames.Div;
      output.TagMode = TagMode.StartTagAndEndTag;

      if (string.IsNullOrEmpty(this.Href))
        output.Attributes.SetAttribute(AttributeNames.Class, "node__header" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));

      else
      {
        output.Attributes.SetAttribute(AttributeNames.Class, "node__header node__header--interactive" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));
        output.Attributes.SetAttribute(AttributeNames.OnClick, $"location.href = '{this.Href}';");
      }

      context.Items["IsNodeHeader"] = true;
    }
  }
}