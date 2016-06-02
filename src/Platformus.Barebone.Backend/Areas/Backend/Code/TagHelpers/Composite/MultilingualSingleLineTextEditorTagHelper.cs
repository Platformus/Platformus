// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Barebone.Backend
{
  [HtmlTargetElement("multilingual-single-line-text-editor", Attributes = ForAttributeName + "," + LocalizationsAttributeName)]
  public class MultilingualSingleLineTextEditorTagHelper : TextBoxTagHelperBase
  {
    private const string ForAttributeName = "asp-for";
    private const string LocalizationsAttributeName = "asp-localizations";

    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }

    [HtmlAttributeName(ForAttributeName)] 
    public ModelExpression For { get; set; }

    [HtmlAttributeName(LocalizationsAttributeName)]
    public IEnumerable<Localization> Localizations { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.For == null)
        return;

      output.SuppressOutput();
      output.Content.Clear();
      output.Content.AppendHtml(this.GenerateField());
    }

    private TagBuilder GenerateField()
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("field");
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(
        new CompositeHtmlContent(
          this.GenerateLabel(this.For),
          this.GenerateInputs()
        )
      );

      return tb;
    }

    private CompositeHtmlContent GenerateInputs()
    {
      List<TagBuilder> tbs = new List<TagBuilder>();

      foreach (Localization localization in this.Localizations)
      {
        tbs.Add(this.GenerateCulture(localization));
        tbs.Add(this.GenerateInput(this.ViewContext, this.For, localization));

        if (localization != this.Localizations.Last())
          tbs.Add(this.GenerateMultilingualSeparator());
      }

      return new CompositeHtmlContent(tbs.ToArray());
    }
  }
}