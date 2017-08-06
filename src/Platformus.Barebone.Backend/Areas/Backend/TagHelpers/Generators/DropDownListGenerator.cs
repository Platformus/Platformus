// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Platformus.Barebone.Primitives;

namespace Platformus.Barebone.Backend
{
  public class DropDownListGenerator : GeneratorBase
  {
    public TagBuilder GenerateDropDownList(ViewContext viewContext, ModelExpression modelExpression, IEnumerable<Option> options, TagHelperAttributeList attributes, string additionalCssClass = null)
    {
      TagBuilder tb = new TagBuilder("div");

      if (!string.IsNullOrEmpty(additionalCssClass))
        tb.AddCssClass(additionalCssClass);

      tb.AddCssClass("drop-down-list");

      if (!this.IsValid(viewContext, modelExpression))
        tb.AddCssClass("input-validation-error");

      tb.MergeAttribute("id", this.GetIdentity(modelExpression));
      this.MergeRequiredAttribute(tb, modelExpression, "drop-down-list--required");
      this.MergeOtherAttribute(tb, attributes);
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(this.GenerateSelectedDropDownListItem(viewContext, modelExpression, options));
      tb.InnerHtml.AppendHtml(this.GenerateDropDownListItems(options));
      tb.InnerHtml.AppendHtml(this.GenerateInput(viewContext, modelExpression, options));
      return tb;
    }

    private TagBuilder GenerateSelectedDropDownListItem(ViewContext viewContext, ModelExpression modelExpression, IEnumerable<Option> options)
    {
      TagBuilder tb = new TagBuilder("a");

      tb.AddCssClass("drop-down-list__item drop-down-list__item--selected");
      tb.MergeAttribute("href", "#");
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(this.GetSelectedOptionText(viewContext, modelExpression, options));
      return tb;
    }

    private TagBuilder GenerateDropDownListItems(IEnumerable<Option> options)
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("drop-down-list__items");
      tb.InnerHtml.Clear();

      foreach (Option option in options)
        tb.InnerHtml.AppendHtml(this.GenerateDropDownListItem(option));

      return tb;
    }

    private TagBuilder GenerateDropDownListItem(Option option)
    {
      TagBuilder tb = new TagBuilder("a");

      tb.AddCssClass("drop-down-list__item");
      tb.MergeAttribute("data-value", option.Value);
      tb.MergeAttribute("href", "#");
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