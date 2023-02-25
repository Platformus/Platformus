// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend;

public class MultilingualHtmlFieldTagHelper : MultilingualFieldTagHelperBase<string>
{
  public Sizes Height { get; set; } = Sizes.Large;

  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    if ((this.For == null && string.IsNullOrEmpty(this.Id)) || this.Localizations == null)
      return;

    this.Class += " form__field--unlimited";
    base.Process(context, output);

    foreach (Localization localization in this.Localizations)
    {
      if (localization.Culture.Id != NeutralCulture.Id)
      {
        output.Content.AppendHtml(this.CreateCulture(localization));
        output.Content.AppendHtml(this.CreateTextArea(localization));
        output.Content.AppendHtml(this.CreateValidationErrorMessage(localization));
      }
    }
  }

  private TagBuilder CreateTextArea(Localization localization)
  {
    TagBuilder tb = TextAreaGenerator.Generate(
      this.GetIdentity(localization),
      value: this.GetValue(localization),
      validation: this.GetValidation(localization)
    );

    tb.AddCssClass("field__text-area field__multilingual");

    if (this.Height == Sizes.Medium)
      tb.AddCssClass("field__text-area--medium");

    else if (this.Height == Sizes.Small)
      tb.AddCssClass("field__text-area--small");

    tb.MergeAttribute(AttributeNames.Lang, localization.Culture.Id);
    tb.MergeAttribute(AttributeNames.DataType, "html");

    if (this.IsDisabled())
      tb.MergeAttribute(AttributeNames.Disabled, "disabled");

    // TODO: merge all the attributes, not only "onchange"
    if (!string.IsNullOrEmpty(this.OnChange))
      tb.MergeAttribute(AttributeNames.OnChange, this.OnChange);

    return tb;
  }
}