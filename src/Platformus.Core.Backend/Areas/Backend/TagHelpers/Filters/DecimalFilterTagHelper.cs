// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend;

public class DecimalFilterTagHelper : CriterionTagHelperBase
{
  public Sizes Width { get; set; } = Sizes.Small;

  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    base.Process(context, output);
    output.Content.AppendHtml(this.CreateNumericBox());
  }

  private TagBuilder CreateNumericBox()
  {
    TagBuilder tb = NumericBoxGenerator.Generate(
      string.Empty,
      NumericTypes.Decimal,
      value: this.GetValue()
    );

    tb.AddCssClass("filter__criterion");

    if (this.Width == Sizes.Small)
      tb.AddCssClass("filter__criterion--small");

    else if (this.Width == Sizes.Medium)
      tb.AddCssClass("filter__criterion--medium");

    else if (this.Width == Sizes.Large)
      tb.AddCssClass("filter__criterion--large");

    tb.MergeAttribute("data-property-path", this.PropertyPath?.ToLower());
    tb.MergeAttribute(AttributeNames.Placeholder, this.Label);
    return tb;
  }
}