// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend;

public class TitleBarTagHelper : TagHelper
{
  [HtmlAttributeNotBound]
  [ViewContext]
  public ViewContext ViewContext { get; set; }

  public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
  {
    output.TagName = TagNames.Div;
    output.TagMode = TagMode.StartTagAndEndTag;
    output.Attributes.SetAttribute(AttributeNames.Class, "title-bar");
    output.Content.SetHtmlContent(this.CreateTitle());
    output.Content.AppendHtml(await this.CreateButtonsAsync(output));
  }

  private TagBuilder CreateTitle()
  {
    TagBuilder tb = new TagBuilder(TagNames.H1);

    tb.AddCssClass("title-bar__title heading heading--h1");

    if (this.ViewContext.ViewBag.Title is string)
      tb.InnerHtml.Append(this.ViewContext.ViewBag.Title);

    else if (this.ViewContext.ViewBag.Title is LocalizedHtmlString)
      tb.InnerHtml.Append((this.ViewContext.ViewBag.Title as LocalizedHtmlString).Value);

    return tb;
  }

  private async Task<TagBuilder> CreateButtonsAsync(TagHelperOutput output)
  {
    TagBuilder tb = new TagBuilder(TagNames.Div);

    tb.AddCssClass("title-bar__buttons buttons");

    TagHelperContent content = await output.GetChildContentAsync();

    tb.InnerHtml.AppendHtml(content);
    return tb;
  }
}