// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend;

public static class RadioButtonListGenerator
{
  public static TagBuilder Generate(string identity, IEnumerable<Option> options, string value = null)
  {
    TagBuilder tb = new TagBuilder(TagNames.Div);

    tb.AddCssClass("radio-button-list");
    tb.MergeAttribute(AttributeNames.Id, identity);

    Option selectedOption = GetSelectedOption(options, value);

    foreach (Option option in options)
      tb.InnerHtml.AppendHtml(GenerateRadioButton(option, option.Value == selectedOption.Value));

    tb.InnerHtml.AppendHtml(GenerateInput(identity, options, value));
    return tb;
  }

  private static TagBuilder GenerateRadioButton(Option option, bool isChecked)
  {
    TagBuilder tb = new TagBuilder(TagNames.A);

    tb.AddCssClass("radio-button-list__radio-button radio-button");
    tb.MergeAttribute(AttributeNames.Href, "#");
    tb.MergeAttribute("data-value", option.Value);
    tb.InnerHtml.AppendHtml(GenerateIndicator(option, isChecked));
    tb.InnerHtml.AppendHtml(GenerateLabel(option));
    return tb;
  }

  private static TagBuilder GenerateIndicator(Option option, bool isChecked)
  {
    TagBuilder tb = new TagBuilder(TagNames.Div);

    tb.AddCssClass("radio-button__indicator");

    if (isChecked)
      tb.AddCssClass("radio-button__indicator--checked");

    return tb;
  }

  private static TagBuilder GenerateLabel(Option option)
  {
    TagBuilder tb = new TagBuilder(TagNames.Div);

    tb.AddCssClass("radio-button__label");
    tb.InnerHtml.AppendHtml(option.Text);
    return tb;
  }

  private static TagBuilder GenerateInput(string identity, IEnumerable<Option> options, string value)
  {
    TagBuilder tb = new TagBuilder(TagNames.Input);

    tb.TagRenderMode = TagRenderMode.SelfClosing;
    tb.MergeAttribute(AttributeNames.Name, identity);
    tb.MergeAttribute(AttributeNames.Type, InputTypes.Hidden);
    tb.MergeAttribute(AttributeNames.Value, GetSelectedOption(options, value)?.Value);
    return tb;
  }

  private static Option GetSelectedOption(IEnumerable<Option> options, string value)
  {
    Option option = options.FirstOrDefault(o => o.Value == value);

    return option ?? options.FirstOrDefault();
  }
}