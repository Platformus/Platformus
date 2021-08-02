// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend
{
  public class MultilingualSingleLineTextFieldTagHelper : TagHelper
  {
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }
    public string Class { get; set; }
    public ModelExpression For { get; set; }
    public IEnumerable<Localization> Localizations { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.For == null)
        return;

      output.TagMode = TagMode.StartTagAndEndTag;
      output.TagName = TagNames.Div;
      output.Attributes.SetAttribute(AttributeNames.Class, "form__field field field--multilingual" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));
      output.Content.AppendHtml(this.CreateLabel());

      foreach (Localization localization in this.Localizations)
      {
        if (localization.Culture.Id != NeutralCulture.Id)
        {
          output.Content.AppendHtml(this.CreateCulture(localization));
          output.Content.AppendHtml(this.CreateTextBox(localization));
          output.Content.AppendHtml(this.CreateValidationErrorMessage(localization));

          if (localization != this.Localizations.Last())
            output.Content.AppendHtml(this.CreateMultilingualSeparator());
        }
      }
    }

    private TagBuilder CreateLabel()
    {
      return FieldGenerator.GenerateLabel(this.For.GetLabel(), this.For.GetIdentity());
    }

    private TagBuilder CreateCulture(Localization localization)
    {
      return FieldGenerator.GenerateCulture(localization);
    }

    private TagBuilder CreateTextBox(Localization localization)
    {
      TagBuilder tb = TextBoxGenerator.Generate(
        this.For.GetIdentity(localization),
        InputTypes.Text,
        this.For.GetValue(this.ViewContext, localization),
        this.For.HasRequiredAttribute(),
        this.For.HasStringLengthAttribute() ? this.For.GetMaxStringLength() : null,
        this.For.IsValid(this.ViewContext, localization)
      );

      tb.AddCssClass("field__text-box");
      tb.MergeAttribute("data-culture", localization.Culture.Id);
      return tb;
    }

    private TagBuilder CreateValidationErrorMessage(Localization localization)
    {
      return ValidationErrorMessageGenerator.Generate(this.For.GetIdentity(localization));
    }

    private TagBuilder CreateMultilingualSeparator()
    {
      return FieldGenerator.GenerateMultilingualSeparator();
    }
  }
}