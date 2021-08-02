// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Platformus.Core.Backend
{
  public static class TextBoxGenerator
  {
    public static TagBuilder Generate(string identity, string type, string value = null, bool isRequired = false, int? maxLength = null, bool isValid = true)
    {
      TagBuilder tb = new TagBuilder(TagNames.Input);

      tb.TagRenderMode = TagRenderMode.SelfClosing;
      tb.AddCssClass("text-box");
      tb.MergeAttribute(AttributeNames.Id, identity);
      tb.MergeAttribute(AttributeNames.Name, identity);
      tb.MergeAttribute(AttributeNames.Type, type);

      if (!string.IsNullOrEmpty(value))
        tb.MergeAttribute(AttributeNames.Value, value);

      if (isRequired)
        tb.AddRequiredAttributes("text-box--required");

      if (maxLength != null)
        tb.AddMaxLengthAttributes((int)maxLength);

      if (!isValid)
        tb.AddCssClass("input-validation-error");

      return tb;
    }
  }
}