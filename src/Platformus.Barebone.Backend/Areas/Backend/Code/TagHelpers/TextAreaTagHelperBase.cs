// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Platformus.Barebone.Backend
{
  public abstract class TextAreaTagHelperBase : TagHelperBase
  {
    protected TagBuilder GenerateTextArea(ViewContext viewContext, ModelExpression modelExpression, Localization localization = null)
    {
      TagBuilder tb = new TagBuilder("textarea");

      if (!this.IsValid(viewContext, modelExpression, localization))
        tb.AddCssClass("input-validation-error");

      tb.MergeAttribute("id", this.GetIdentity(modelExpression, localization));
      tb.MergeAttribute("name", this.GetIdentity(modelExpression, localization));

      string value = this.GetValue(viewContext, modelExpression, localization);

      if (!string.IsNullOrEmpty(value))
      {
        tb.InnerHtml.Clear();
        tb.InnerHtml.Append(value);
      }

      this.HandleAttributes(tb, modelExpression);
      return tb;
    }
  }
}