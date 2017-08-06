// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Platformus.Barebone.Primitives;

namespace Platformus.Barebone.Backend
{
  public class TextAreaGenerator : GeneratorBase
  {
    public TagBuilder GenerateTextArea(ViewContext viewContext, ModelExpression modelExpression, TagHelperAttributeList attributes, Localization localization = null, string additionalCssClass = null)
    {
      TagBuilder tb = new TagBuilder("textarea");

      if (!string.IsNullOrEmpty(additionalCssClass))
        tb.AddCssClass(additionalCssClass);

      tb.AddCssClass("text-area");

      if (!this.IsValid(viewContext, modelExpression, localization))
        tb.AddCssClass("input-validation-error");

      tb.MergeAttribute("id", this.GetIdentity(modelExpression, localization));
      tb.MergeAttribute("name", this.GetIdentity(modelExpression, localization));

      if (localization != null)
        tb.MergeAttribute("data-culture", localization.Culture.Code);

      this.MergeRequiredAttribute(tb, modelExpression, "text-area--required");
      this.MergeStringLengthAttribute(tb, modelExpression);
      this.MergeOtherAttribute(tb, attributes);

      string value = this.GetValue(viewContext, modelExpression, localization);

      if (!string.IsNullOrEmpty(value))
      {
        tb.InnerHtml.Clear();
        tb.InnerHtml.Append(value);
      }

      return tb;
    }
  }
}