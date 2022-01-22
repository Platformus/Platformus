// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  public class TabTagHelper : TagHelper
  {
    public string Code { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      output.TagName = TagNames.Div;
      output.TagMode = TagMode.StartTagAndEndTag;
      output.Attributes.SetAttribute(AttributeNames.Class, "tabs__tab");
      output.Attributes.SetAttribute("data-tab-page-id", this.Code);
    }
  }
}