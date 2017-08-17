// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Barebone.Backend
{
  public class CheckboxGenerator : GeneratorBase
  {
    public TagBuilder GenerateCheckbox(ViewContext viewContext, ModelExpression modelExpression, TagHelperAttributeList attributes, string additionalCssClass = null)
    {
      TagBuilder tb = new TagBuilder("a");

      if (!string.IsNullOrEmpty(additionalCssClass))
        tb.AddCssClass(additionalCssClass);

      tb.AddCssClass("checkbox");
      tb.MergeAttribute("id", this.GetIdentity(modelExpression));
      tb.MergeAttribute("href", "#");
      this.MergeOtherAttribute(tb, attributes);
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(this.GenerateIndicator(viewContext, modelExpression));
      tb.InnerHtml.AppendHtml(this.GenerateLabel(modelExpression));
      tb.InnerHtml.AppendHtml(this.GenerateInput(viewContext, modelExpression));
      return tb;
    }

    private TagBuilder GenerateIndicator(ViewContext viewContext, ModelExpression modelExpression)
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("checkbox__indicator");

      if (this.GetValue(viewContext, modelExpression) == true.ToString().ToLower())
        tb.AddCssClass("checkbox__indicator--checked");

      return tb;
    }

    private TagBuilder GenerateLabel(ModelExpression modelExpression)
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("checkbox__label");
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(modelExpression.Metadata.DisplayName);
      return tb;
    }

    private TagBuilder GenerateInput(ViewContext viewContext, ModelExpression modelExpression)
    {
      TagBuilder tb = new TagBuilder("input");

      tb.TagRenderMode = TagRenderMode.SelfClosing;
      tb.MergeAttribute("name", this.GetIdentity(modelExpression));
      tb.MergeAttribute("type", "hidden");
      tb.MergeAttribute("value", this.GetValue(viewContext, modelExpression));
      return tb;
    }

    private string GetValue(ViewContext viewContext, ModelExpression modelExpression)
    {
      string value = base.GetValue(viewContext, modelExpression);

      if (string.IsNullOrEmpty(value))
        return false.ToString().ToLower();

      return value.ToLower();
    }
  }
}