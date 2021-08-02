// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Platformus.Core.Backend
{
  public static class ImageViewGenerator
  {
    public static TagBuilder Generate(string identity, string url)
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("image-view");
      tb.MergeAttribute("id", identity);
      tb.InnerHtml.AppendHtml(GenerateImage(url));
      return tb;
    }

    private static TagBuilder GenerateImage(string url)
    {
      TagBuilder tb = new TagBuilder(TagNames.Img);

      tb.AddCssClass("image-view__image");

      if (string.IsNullOrEmpty(url))
        tb.MergeAttribute(AttributeNames.Style, "display: none;");

      else tb.MergeAttribute(AttributeNames.Src, url);

      return tb;
    }
  }
}