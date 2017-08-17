// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Platformus.Barebone.Primitives;

namespace Platformus.Barebone.Backend
{
  [HtmlTargetElement("radio-button-list-field", Attributes = ForAttributeName + "," + OptionsAttributeName)]
  public class RadioButtonListFieldTagHelper : TagHelper
  {
    private const string ForAttributeName = "asp-for";
    private const string OptionsAttributeName = "asp-options";

    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }

    [HtmlAttributeName(ForAttributeName)] 
    public ModelExpression For { get; set; }

    [HtmlAttributeName(OptionsAttributeName)]
    public IEnumerable<Option> Options { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.ViewContext == null || this.For == null || this.Options == null)
        return;

      output.SuppressOutput();
      output.Content.Clear();
      output.Content.AppendHtml(this.GenerateField(output.Attributes));
    }

    private TagBuilder GenerateField(TagHelperAttributeList attributes)
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("form__field field");
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(new FieldGenerator().GenerateLabel(this.For));
      tb.InnerHtml.AppendHtml(new RadioButtonListGenerator().GenerateRadioButtonList(this.ViewContext, this.For, this.Options, attributes, "field__radio-button-list"));
      return tb;
    }
  }
}