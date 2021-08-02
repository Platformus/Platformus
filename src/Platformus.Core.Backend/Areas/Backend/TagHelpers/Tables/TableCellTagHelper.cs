// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  public class TableCellTagHelper : TagHelper
  {
    public string Class { get; set; }
    public bool IsHeader { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.IsHeader)
      {
        output.TagName = TagNames.TH;
        output.Attributes.SetAttribute(AttributeNames.Class, "table__cell table__cell--header" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));
      }

      else
      {
        output.TagName = TagNames.TD;
        output.Attributes.SetAttribute(AttributeNames.Class, "table__cell" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));
      }
    }
  }
}