// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend;

public class BreadcrumbTagHelper : TagHelper
{
  public string Href { get; set; }

  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    output.TagName = TagNames.A;
    output.TagMode = TagMode.StartTagAndEndTag;
    output.Attributes.SetAttribute(AttributeNames.Class, "breadcrumbs__breadcrumb");
    output.Attributes.SetAttribute(AttributeNames.Href, this.Href);
    output.PostElement.SetHtmlContent(this.CreateSeparator());
  }

  private TagBuilder CreateSeparator()
  {
    TagBuilder tb = new TagBuilder(TagNames.Div);

    tb.AddCssClass("breadcrumbs__separator");
    tb.InnerHtml.AppendHtml("/");
    return tb;
  }
}