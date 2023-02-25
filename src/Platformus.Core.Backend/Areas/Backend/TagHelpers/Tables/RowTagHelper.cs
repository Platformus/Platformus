// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend;

[RestrictChildren("cell", "image-cell", "row-controls", "partial")]
public class RowTagHelper : TagHelper
{
  public class RowControls
  {
    public TagHelperContent Content { get; set; }
  }

  [HtmlAttributeNotBound]
  [ViewContext]
  public ViewContext ViewContext { get; set; }
  public string Class { get; set; }
  public string Href { get; set; }

  public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
  {
    context.Items["RowControls"] = new RowControls();

    TagHelperContent rowContent = await output.GetChildContentAsync();
    RowControls rowControls = context.Items["RowControls"] as RowControls;

    output.TagName = TagNames.TR;

    if (string.IsNullOrEmpty(this.Href))
      output.Attributes.SetAttribute(AttributeNames.Class, "table__row" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));

    else
    {
      output.Attributes.SetAttribute(AttributeNames.Class, "table__row table__row--interactive" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));
      output.Attributes.SetAttribute(AttributeNames.OnClick, $"location.href = '{this.ViewContext.HttpContext.Request.CombineUrl(this.Href)}';");
    }

    if (rowControls.Content != null)
    {
      string rowControlsContentHtml = rowControls.Content.GetContent();

      if (!string.IsNullOrEmpty(rowControlsContentHtml))
      {
        string rowContentHtml = rowContent.GetContent();

        // TODO: looks ugly; should be refactored
        rowContentHtml = rowContentHtml.Insert(
          rowContentHtml.LastIndexOf("</td>"),
          $"<div class=\"table__row-controls buttons buttons--minor\">{rowControls.Content.GetContent()}</div>"
        );

        output.Content.SetHtmlContent(rowContentHtml);
      }
    }
  }
}