﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend
{
  [HtmlTargetElement("multilingual-multiline-text-field", Attributes = ForAttributeName + "," + LocalizationsAttributeName + "," + HeightAttributeName)]
  public class MultilingualMultilineTextFieldTagHelper : TagHelper
  {
    private const string ForAttributeName = "asp-for";
    private const string LocalizationsAttributeName = "asp-localizations";
    private const string HeightAttributeName = "asp-height";

    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }

    [HtmlAttributeName(ForAttributeName)] 
    public ModelExpression For { get; set; }

    [HtmlAttributeName(LocalizationsAttributeName)]
    public IEnumerable<Localization> Localizations { get; set; }

    [HtmlAttributeName(HeightAttributeName)]
    public string Height { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.For == null)
        return;

      output.SuppressOutput();
      output.Content.Clear();
      output.Content.AppendHtml(this.GenerateField(output.Attributes));
    }

    private TagBuilder GenerateField(TagHelperAttributeList attributes)
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("form__field field field--multilingual");

      FieldGenerator fieldGenerator = new FieldGenerator();

      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(fieldGenerator.GenerateLabel(this.For));

      foreach (Localization localization in this.Localizations)
      {
        if (localization.Culture.Id != NeutralCulture.Id)
        {
          tb.InnerHtml.AppendHtml(fieldGenerator.GenerateCulture(localization, true));
          tb.InnerHtml.AppendHtml(new TextAreaGenerator().GenerateTextArea(this.ViewContext, this.For, attributes, localization, "field__text-area field__text-area--multilingual" + (this.Height == "small" ? " field__text-area--small" : null)));
          tb.InnerHtml.AppendHtml(new ValidationErrorMessageGenerator().GenerateValidationErrorMessage(this.For, localization));

          if (localization != this.Localizations.Last())
            tb.InnerHtml.AppendHtml(fieldGenerator.GenerateMultilingualSeparator());
        }
      }

      return tb;
    }
  }
}