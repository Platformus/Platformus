// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Platformus.Core.Backend
{
  public static class CheckboxGenerator
  {
    public static TagBuilder Generate(string identity, string label, bool value = false)
    {
      TagBuilder tb = new TagBuilder(TagNames.A);

      tb.AddCssClass("checkbox");
      tb.MergeAttribute(AttributeNames.Id, identity);
      tb.MergeAttribute(AttributeNames.Href, "#");
      tb.InnerHtml.AppendHtml(GenerateIndicator(value));
      tb.InnerHtml.AppendHtml(GenerateLabel(label));
      tb.InnerHtml.AppendHtml(GenerateInput(identity, value));
      return tb;
    }

    private static TagBuilder GenerateIndicator(bool value)
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("checkbox__indicator");

      if (value)
        tb.AddCssClass("checkbox__indicator--checked");

      return tb;
    }

    private static TagBuilder GenerateLabel(string text)
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("checkbox__label");
      tb.InnerHtml.AppendHtml(text);
      return tb;
    }

    private static TagBuilder GenerateInput(string identity, bool value)
    {
      TagBuilder tb = new TagBuilder(TagNames.Input);

      tb.TagRenderMode = TagRenderMode.SelfClosing;
      tb.MergeAttribute(AttributeNames.Name, identity);
      tb.MergeAttribute(AttributeNames.Type, InputTypes.Hidden);
      tb.MergeAttribute(AttributeNames.Value, value.ToString().ToLower());
      return tb;
    }
  }
}