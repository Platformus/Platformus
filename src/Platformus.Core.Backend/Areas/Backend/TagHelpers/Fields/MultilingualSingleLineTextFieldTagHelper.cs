// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend;

public class MultilingualSingleLineTextFieldTagHelper : MultilingualFieldTagHelperBase<string>
{
  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    if ((this.For == null && string.IsNullOrEmpty(this.Id)) || this.Localizations == null)
      return;

    base.Process(context, output);

    foreach (Localization localization in this.Localizations)
    {
      if (localization.Culture.Id != NeutralCulture.Id)
      {
        output.Content.AppendHtml(this.CreateCulture(localization));
        output.Content.AppendHtml(this.CreateTextBox(localization));
        output.Content.AppendHtml(this.CreateValidationErrorMessage(localization));
      }
    }
  }

  private TagBuilder CreateTextBox(Localization localization)
  {
    TagBuilder tb = TextBoxGenerator.Generate(
      this.GetIdentity(localization),
      InputTypes.Text,
      value: this.GetValue(localization),
      validation: this.GetValidation(localization)
    );

    tb.AddCssClass("field__text-box field__multilingual");
    tb.MergeAttribute(AttributeNames.Lang, localization.Culture.Id);

    if (this.IsDisabled())
      tb.MergeAttribute(AttributeNames.Disabled, "disabled");

    // TODO: merge all the attributes, not only "onchange"
    if (!string.IsNullOrEmpty(this.OnChange))
      tb.MergeAttribute(AttributeNames.OnChange, this.OnChange);

    return tb;
  }
}