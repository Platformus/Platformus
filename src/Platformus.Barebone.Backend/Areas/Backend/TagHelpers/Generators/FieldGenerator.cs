// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Platformus.Barebone.Primitives;

namespace Platformus.Barebone.Backend
{
  public class FieldGenerator : GeneratorBase
  {
    public TagBuilder GenerateLabel(ModelExpression modelExpression)
    {
      TagBuilder tb = new TagBuilder("label");

      tb.AddCssClass("field__label label");
      tb.MergeAttribute("for", this.GetIdentity(modelExpression));
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(modelExpression.Metadata.DisplayName);
      return tb;
    }

    public TagBuilder GenerateCulture(Localization localization)
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("field__culture");
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(this.GenerateFlag(localization));
      return tb;
    }

    public TagBuilder GenerateFlag(Localization localization)
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("field__culture-flag");
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(localization.Culture.Code);
      return tb;
    }

    public TagBuilder GenerateMultilingualSeparator()
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("field__multilingual-separator");
      return tb;
    }
  }
}