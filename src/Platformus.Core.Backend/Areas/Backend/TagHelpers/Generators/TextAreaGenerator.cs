// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Platformus.Core.Backend
{
  public static class TextAreaGenerator
  {
    public static TagBuilder Generate(string identity, string value = null, bool isRequired = false, int? maxLength = null, bool isValid = true)
    {
      TagBuilder tb = new TagBuilder(TagNames.TextArea);

      tb.AddCssClass("text-area");
      tb.MergeAttribute(AttributeNames.Id, identity);
      tb.MergeAttribute(AttributeNames.Name, identity);
      if (!string.IsNullOrEmpty(value))
        tb.InnerHtml.Append(value);

      if (isRequired)
        tb.AddRequiredAttributes("text-area--required");

      if (maxLength != null)
        tb.AddMaxLengthAttributes((int)maxLength);

      if (!isValid)
        tb.AddCssClass("input-validation-error");

      return tb;
    }
  }
}