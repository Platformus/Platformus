// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Platformus.Barebone.Backend
{
  public class TextBoxGenerator : GeneratorBase
  {
    public TagBuilder GenerateTextBox(ViewContext viewContext, ModelExpression modelExpression, Localization localization = null, string type = "text", string additionalCssClass = null)
    {
      TagBuilder tb = new TagBuilder("input");

      tb.TagRenderMode = TagRenderMode.SelfClosing;

      if (!string.IsNullOrEmpty(additionalCssClass))
        tb.AddCssClass(additionalCssClass);

      tb.AddCssClass("text-box");

      if (!this.IsValid(viewContext, modelExpression, localization))
        tb.AddCssClass("input-validation-error");

      tb.MergeAttribute("id", this.GetIdentity(modelExpression, localization));
      tb.MergeAttribute("name", this.GetIdentity(modelExpression, localization));
      tb.MergeAttribute("type", type);

      string value = this.GetValue(viewContext, modelExpression, localization);

      if (!string.IsNullOrEmpty(value))
        tb.MergeAttribute("value", value);

      this.MergeRequiredAttribute(tb, modelExpression, "text-box--required");
      this.MergeStringLengthAttribute(tb, modelExpression);
      return tb;
    }
  }
}