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
    public static TagBuilder Generate(string identity, IEnumerable<Option> options, string value = null, Validation validation = null)
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("drop-down-list");
      tb.MergeAttribute(AttributeNames.Id, identity);

      if (validation?.IsValid == false)
        tb.AddCssClass("input-validation-error");

      tb.InnerHtml.AppendHtml(GenerateSelectedDropDownListItem(GetSelectedOption(options, value)));
      tb.InnerHtml.AppendHtml(GenerateDropDownListItems(options));
      tb.InnerHtml.AppendHtml(GenerateInput(identity, options, value, validation));
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
      {
        tb.InnerHtml.AppendHtml(GenerateDropDownListItem(option));

        if (option.Options != null)
        {
          TagBuilder tbLevel = new TagBuilder(TagNames.Div);

          tbLevel.AddCssClass("drop-down-list__node");
          tbLevel.InnerHtml.AppendHtml(GenerateDropDownListItems(option.Options).InnerHtml);
          tb.InnerHtml.AppendHtml(tbLevel);
        }
      }

      return tb;
    }

    private static TagBuilder GenerateDropDownListItem(Option option)
    {
      TagBuilder tb = new TagBuilder(TagNames.A);

      tb.AddCssClass("drop-down-list__item");
      tb.MergeAttribute(AttributeNames.Href, "#");
      tb.MergeAttribute("data-value", option.Value );
      tb.InnerHtml.AppendHtml(option.Text);
      return tb;
    }

    private static TagBuilder GenerateInput(string identity, IEnumerable<Option> options, string value, Validation validation)
    {
      TagBuilder tb = new TagBuilder(TagNames.Input);

      tb.TagRenderMode = TagRenderMode.SelfClosing;
      tb.MergeAttribute(AttributeNames.Name, identity);
      tb.MergeAttribute(AttributeNames.Type, InputTypes.Hidden);
      tb.MergeAttribute(AttributeNames.Value, GetSelectedOption(options, value)?.Value);

      if (validation?.IsRequired == true)
        tb.AddRequiredAttributes(validation.IsRequiredValidationErrorMessage);

      return tb;
    }

    private static Option GetSelectedOption(IEnumerable<Option> options, string value)
    {
      Option option = options.FirstOrDefault(o => o.Value == value);

      return option ?? options.FirstOrDefault();
    }
  }
}