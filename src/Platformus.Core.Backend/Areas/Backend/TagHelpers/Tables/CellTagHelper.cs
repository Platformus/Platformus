// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend;

public class CellTagHelper : TagHelper
{
  public string Class { get; set; }
  public bool IsHeader { get; set; }
  public bool IsHighlighted { get; set; }

  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    if (this.IsHeader)
      output.TagName = TagNames.TH;

    else output.TagName = TagNames.TD;

    output.TagMode = TagMode.StartTagAndEndTag;
    output.Attributes.SetAttribute(
      AttributeNames.Class,
      "table__cell" +
      (this.IsHeader ? " table__cell--header" : null) +
      (this.IsHighlighted ? " table__cell--highlighted" : null) +
      (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}")
    );
  }
}