// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend
{
  public class DropDownListFilterTagHelper : CriterionTagHelperBase
  {
    public IEnumerable<Option> Options { get; set; }
    public Sizes Width { get; set; } = Sizes.Large;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.Options == null)
        return;

      base.Process(context, output);
      output.Content.AppendHtml(this.CreateDropDownList());
    }

    protected override string GetValue()
    {
      return (this.Options.FirstOrDefault(o => o.Value == base.GetValue()) ?? this.Options.FirstOrDefault())?.Value;
    }

    private TagBuilder CreateDropDownList()
    {
      TagBuilder tb = DropDownListGenerator.Generate(
        string.Empty,
        this.Options,
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
      return tb;
    }
  }
}