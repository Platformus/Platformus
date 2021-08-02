// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Platformus.Core.Backend
{
  public static class CheckboxGenerator
  {
    public static TagBuilder Generate(string identity, string label, bool isChecked)
    {
      TagBuilder tb = new TagBuilder(TagNames.A);

      tb.AddCssClass("checkbox");
      tb.MergeAttribute(AttributeNames.Id, identity);
      tb.MergeAttribute(AttributeNames.Href, "#");
      tb.InnerHtml.AppendHtml(GenerateIndicator(isChecked));
      tb.InnerHtml.AppendHtml(GenerateLabel(label));
      tb.InnerHtml.AppendHtml(GenerateInput(identity, isChecked));
      return tb;
    }

    private static TagBuilder GenerateIndicator(bool isChecked)
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("checkbox__indicator");

      if (isChecked)
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

    private static TagBuilder GenerateInput(string identity, bool isChecked)
    {
      TagBuilder tb = new TagBuilder(TagNames.Input);

      tb.TagRenderMode = TagRenderMode.SelfClosing;
      tb.MergeAttribute(AttributeNames.Name, identity);
      tb.MergeAttribute(AttributeNames.Type, "hidden");
      tb.MergeAttribute(AttributeNames.Value, isChecked.ToString().ToLower());
      return tb;
    }
  }
}