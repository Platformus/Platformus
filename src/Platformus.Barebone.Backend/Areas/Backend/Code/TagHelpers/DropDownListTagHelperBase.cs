// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Mvc.Rendering;

namespace Platformus.Barebone.Backend
{
  public abstract class DropDownListTagHelperBase : TagHelperBase
  {
    protected TagBuilder GenerateDropDownList(ViewContext viewContext, ModelExpression modelExpression, IEnumerable<Option> options)
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("drop-down-list");

      if (!this.IsValid(viewContext, modelExpression))
        tb.AddCssClass("input-validation-error");

      tb.MergeAttribute("id", this.GetIdentity(modelExpression));
      this.HandleAttributes(tb, modelExpression);
      tb.InnerHtml.Clear();
      tb.InnerHtml.Append(
        new CompositeHtmlContent(
          this.GenerateText(viewContext, modelExpression, options),
          this.GenerateDropDownListItems(options),
          this.GenerateInput(viewContext, modelExpression, options)
        )
      );

      return tb;
    }

    private TagBuilder GenerateText(ViewContext viewContext, ModelExpression modelExpression, IEnumerable<Option> options)
    {
      TagBuilder tb = new TagBuilder("a");

      tb.AddCssClass("text");
      tb.Attributes.Add("href", "#");
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(this.GetText(viewContext, modelExpression, options));
      return tb;
    }

    private TagBuilder GenerateDropDownListItems(IEnumerable<Option> options)
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("drop-down-list-items");
      tb.InnerHtml.Clear();
      tb.InnerHtml.Append(new CompositeHtmlContent(options.Select(o => this.GenerateDropDownListItem(o)).ToArray()));
      return tb;
    }

    private TagBuilder GenerateDropDownListItem(Option option)
    {
      TagBuilder tb = new TagBuilder("a");

      tb.AddCssClass("drop-down-list-item");
      tb.Attributes.Add("data-value", option.Value);
      tb.Attributes.Add("href", "#");
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(option.Text);
      return tb;
    }

    private TagBuilder GenerateInput(ViewContext viewContext, ModelExpression modelExpression, IEnumerable<Option> options)
    {
      TagBuilder tb = new TagBuilder("input");

      tb.Attributes.Add("name", this.GetIdentity(modelExpression));
      tb.Attributes.Add("type", "hidden");
      tb.Attributes.Add("value", this.GetValue(viewContext, modelExpression, options));
      return tb;
    }

    private string GetText(ViewContext viewContext, ModelExpression modelExpression, IEnumerable<Option> options)
    {
      Option option = this.GetOption(viewContext, modelExpression, options);

      if (option == null)
        return null;

      return option.Text;
    }

    private string GetValue(ViewContext viewContext, ModelExpression modelExpression, IEnumerable<Option> options)
    {
      Option option = this.GetOption(viewContext, modelExpression, options);

      if (option == null)
        return null;

      return option.Value;
    }

    private Option GetOption(ViewContext viewContext, ModelExpression modelExpression, IEnumerable<Option> options)
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