// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Platformus.Barebone.Primitives;

namespace Platformus.Barebone.Backend
{
  public class RadioButtonListGenerator : GeneratorBase
  {
    public TagBuilder GenerateRadioButtonList(ViewContext viewContext, ModelExpression modelExpression, IEnumerable<Option> options, TagHelperAttributeList attributes, string additionalCssClass = null)
    {
      TagBuilder tb = new TagBuilder("div");

      if (!string.IsNullOrEmpty(additionalCssClass))
        tb.AddCssClass(additionalCssClass);

      tb.AddCssClass("radio-button-list");
      tb.MergeAttribute("id", this.GetIdentity(modelExpression));
      this.MergeOtherAttribute(tb, attributes);
      tb.InnerHtml.Clear();

      Option selectedOption = this.GetSelectedOption(viewContext, modelExpression, options);

      foreach (Option option in options)
        tb.InnerHtml.AppendHtml(this.GenerateRadioButton(option, selectedOption));

      tb.InnerHtml.AppendHtml(this.GenerateInput(viewContext, modelExpression, options));
      return tb;
    }

    private TagBuilder GenerateRadioButton(Option option, Option selectedOption)
    {
      TagBuilder tb = new TagBuilder("a");

      tb.AddCssClass("radio-button-list__radio-button radio-button");
      tb.MergeAttribute("href", "#");
      tb.MergeAttribute("data-value", option.Value);
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(this.GenerateIndicator(option, selectedOption));
      tb.InnerHtml.AppendHtml(this.GenerateLabel(option));
      return tb;
    }

    private TagBuilder GenerateIndicator(Option option, Option selectedOption)
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("radio-button__indicator");

      if (option.Value == selectedOption.Value)
        tb.AddCssClass("radio-button__indicator--checked");

      return tb;
    }

    private TagBuilder GenerateLabel(Option option)
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("radio-button__label");
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(option.Text);
      return tb;
    }

    private TagBuilder GenerateInput(ViewContext viewContext, ModelExpression modelExpression, IEnumerable<Option> options)
    {
      TagBuilder tb = new TagBuilder("input");

      tb.TagRenderMode = TagRenderMode.SelfClosing;
      tb.MergeAttribute("name", this.GetIdentity(modelExpression));
      tb.MergeAttribute("type", "hidden");
      tb.MergeAttribute("value", this.GetSelectedOptionValue(viewContext, modelExpression, options));
      this.MergeRequiredAttribute(tb, modelExpression, null);
      return tb;
    }

    private string GetSelectedOptionText(ViewContext viewContext, ModelExpression modelExpression, IEnumerable<Option> options)
    {
      Option option = this.GetSelectedOption(viewContext, modelExpression, options);

      if (option == null)
        return null;

      return option.Text;
    }

    private string GetSelectedOptionValue(ViewContext viewContext, ModelExpression modelExpression, IEnumerable<Option> options)
    {
      Option option = this.GetSelectedOption(viewContext, modelExpression, options);

      if (option == null)
        return null;

      return option.Value;
    }

    private Option GetSelectedOption(ViewContext viewContext, ModelExpression modelExpression, IEnumerable<Option> options)
    {
      string value = this.GetValue(viewContext, modelExpression);
      Option option = null;

      if (!string.IsNullOrEmpty(value))
        option = options.FirstOrDefault(o => o.Value == value);

      if (option == null)
        if (modelExpression.Model != null)
          option = options.FirstOrDefault(o => o.Value == modelExpression.Model.ToString());

      if (option == null)
        option = options.FirstOrDefault();

      return option;
    }
  }
}