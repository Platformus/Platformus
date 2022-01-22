// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Platformus.Core.Backend
{
  public static class ValidationErrorMessageGenerator
  {
    public static TagBuilder Generate(string identity, bool isValid, string text = null)
    {
      TagBuilder tb = new TagBuilder(TagNames.Span);

      if (isValid)
        tb.AddCssClass("field-validation-valid");

      else tb.AddCssClass("field-validation-error");

      tb.MergeAttribute("data-valmsg-for", identity);
      tb.MergeAttribute("data-valmsg-replace", "true");

      if (!string.IsNullOrEmpty(text))
        tb.InnerHtml.AppendHtml(text);

      return tb;
    }
  }
}