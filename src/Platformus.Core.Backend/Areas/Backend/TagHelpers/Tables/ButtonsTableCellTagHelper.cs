// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  public class ButtonsTableCellTagHelper : TagHelper
  {
    public string Class { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      TagHelperContent content = output.GetChildContentAsync().Result;

      output.TagName = TagNames.TD;
      output.Attributes.SetAttribute(AttributeNames.Class, "table__cell table__cell--context-controls" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));
      output.Content.Clear();
      output.Content.AppendHtml(this.CreateButtons(content));
    }

    public TagBuilder CreateButtons(TagHelperContent content)
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("table__buttons buttons");
      tb.InnerHtml.AppendHtml(content);
      return tb;
    }
  }
}