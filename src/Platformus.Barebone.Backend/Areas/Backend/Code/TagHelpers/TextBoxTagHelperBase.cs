// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Platformus.Barebone.Backend
{
  public abstract class TextBoxTagHelperBase : TagHelperBase
  {
    protected TagBuilder GenerateInput(ViewContext viewContext, ModelExpression modelExpression, Localization localization = null, string type = "text")
    {
      TagBuilder tb = new TagBuilder("input");

      if (!this.IsValid(viewContext, modelExpression, localization))
        tb.AddCssClass("input-validation-error");

      tb.MergeAttribute("id", this.GetIdentity(modelExpression, localization));
      tb.MergeAttribute("name", this.GetIdentity(modelExpression, localization));
      tb.MergeAttribute("type", type);

      string value = this.GetValue(viewContext, modelExpression, localization);

      if (!string.IsNullOrEmpty(value))
        tb.MergeAttribute("value", value);

      this.HandleAttributes(tb, modelExpression);
      tb.TagRenderMode = TagRenderMode.SelfClosing;
      return tb;
    }
  }
}