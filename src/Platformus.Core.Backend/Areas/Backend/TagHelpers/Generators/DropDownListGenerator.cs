// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend
{
  public static class DropDownListGenerator
  {
    public static TagBuilder Generate(string identity, IEnumerable<Option> options, string value = null, bool isRequired = false, bool isValid = true)
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("drop-down-list");
      tb.MergeAttribute(AttributeNames.Id, identity);

      if (isRequired)
        tb.AddRequiredAttributes("drop-down-list--required");

      if (!isValid)
        tb.AddCssClass("input-validation-error");

      tb.InnerHtml.AppendHtml(GenerateSelectedDropDownListItem(GetSelectedOption(options, value)));
      tb.InnerHtml.AppendHtml(GenerateDropDownListItems(options));
      tb.InnerHtml.AppendHtml(GenerateInput(identity, options, value, isRequired));
      return tb;
    }

    private static TagBuilder GenerateSelectedDropDownListItem(Option option)
    {
      TagBuilder tb = new TagBuilder(TagNames.A);

      tb.AddCssClass("drop-down-list__item drop-down-list__item--selected");
      tb.MergeAttribute(AttributeNames.Href, "#");
      tb.InnerHtml.AppendHtml(option?.Text ?? "&nbsp;");
      return tb;
    }

    private static TagBuilder GenerateDropDownListItems(IEnumerable<Option> options)
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("drop-down-list__items");

      foreach (Option option in options)
        tb.InnerHtml.AppendHtml(GenerateDropDownListItem(option));

      return tb;
    }

    private static TagBuilder GenerateDropDownListItem(Option option)
    {
      TagBuilder tb = new TagBuilder(TagNames.A);

      tb.AddCssClass("drop-down-list__item");
      tb.MergeAttribute(AttributeNames.Href, "#");
      tb.MergeAttribute("data-value", option.Value);
      tb.InnerHtml.AppendHtml(option.Text);
      return tb;
    }

    private static TagBuilder GenerateInput(string identity, IEnumerable<Option> options, string value, bool isRequired)
    {
      TagBuilder tb = new TagBuilder(TagNames.Input);

      tb.TagRenderMode = TagRenderMode.SelfClosing;
      tb.MergeAttribute(AttributeNames.Name, identity);
      tb.MergeAttribute(AttributeNames.Type, "hidden");
      tb.MergeAttribute(AttributeNames.Value, GetSelectedOption(options, value)?.Value);

      if (isRequired)
        tb.AddRequiredAttributes(string.Empty);

      return tb;
    }

    private static Option GetSelectedOption(IEnumerable<Option> options, string value)
    {
      Option option = options.FirstOrDefault(o => o.Value == value);

      return option ?? options.FirstOrDefault();
    }
  }
}