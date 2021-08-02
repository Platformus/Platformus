// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Platformus.Core.Backend
{
  public static class ValidationErrorMessageGenerator
  {
    public static TagBuilder Generate(string identity)
    {
      TagBuilder tb = new TagBuilder("span");

      tb.AddCssClass("field__validation-error");
      tb.AddCssClass("field-validation-valid");
      tb.AddCssClass("validation-error");
      tb.MergeAttribute("data-valmsg-for", identity);
      tb.MergeAttribute("data-valmsg-replace", "true");
      return tb;
    }
  }
}