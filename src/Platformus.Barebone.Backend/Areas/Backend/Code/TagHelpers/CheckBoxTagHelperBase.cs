// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Platformus.Barebone.Backend
{
  public abstract class CheckBoxTagHelperBase : TagHelperBase
  {
    protected TagBuilder GenerateCheckBox(ViewContext viewContext, ModelExpression modelExpression)
    {
      TagBuilder tb = new TagBuilder("a");

      tb.AddCssClass("checkbox");
      tb.MergeAttribute("id", this.GetIdentity(modelExpression));
      tb.MergeAttribute("href", "#");
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(
        new CompositeHtmlContent(
          this.GenerateIndicator(viewContext, modelExpression),
          this.GenerateText(modelExpression),
          this.GenerateInput(viewContext, modelExpression)
        )
      );

      return tb;
    }

    private TagBuilder GenerateIndicator(ViewContext viewContext, ModelExpression modelExpression)
    {
      TagBuilder tb = new TagBuilder("span");

      if (this.GetValue(viewContext, modelExpression) == true.ToString().ToLower())
        tb.Attributes.Add("class", "indicator checked");

      else tb.Attributes.Add("class", "indicator");

      return tb;
    }

    private TagBuilder GenerateText(ModelExpression modelExpression)
    {
      TagBuilder tb = new TagBuilder("span");

      tb.Attributes.Add("class", "text");
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(modelExpression.Metadata.DisplayName);
      return tb;
    }

    private TagBuilder GenerateInput(ViewContext viewContext, ModelExpression modelExpression)
    {
      TagBuilder tb = new TagBuilder("input");

      tb.Attributes.Add("name", this.GetIdentity(modelExpression));
      tb.Attributes.Add("type", "hidden");
      tb.Attributes.Add("value", this.GetValue(viewContext, modelExpression));
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