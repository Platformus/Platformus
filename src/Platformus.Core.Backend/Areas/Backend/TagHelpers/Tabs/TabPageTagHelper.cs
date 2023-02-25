// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend;

public class TabPageTagHelper : TagHelper
{
  public string Code { get; set; }

  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    output.TagName = TagNames.Div;
    output.TagMode = TagMode.StartTagAndEndTag;
    output.Attributes.SetAttribute(AttributeNames.Class, "tab-pages__tab-page");
    output.Attributes.SetAttribute(AttributeNames.Id, $"tabPage{this.Code}");
  }
}