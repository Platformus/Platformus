// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  public class ImageTableCellTagHelper : TagHelper
  {
    public string Class { get; set; }
    public string Url { get; set; }
    public int Width { get; set; } = 100;
    public int Height { get; set; } = 100;
    public string AlternativeText { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      output.TagMode = TagMode.StartTagAndEndTag;
      output.TagName = TagNames.TD;
      output.Attributes.SetAttribute(AttributeNames.Class, "table__cell table__cell--context-controls" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));
      output.Content.AppendHtml(this.CreateImage());
    }

    private TagBuilder CreateImage()
    {
      TagBuilder tb = new TagBuilder(TagNames.Img);

      tb.AddCssClass("table__image");
      tb.MergeAttribute(AttributeNames.Src, $"/img?url={this.Url}&destination.width={this.Width}&destination.height={this.Height}");
      tb.MergeAttribute(AttributeNames.Alt, this.AlternativeText);
      return tb;
    }
  }
}