// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Platformus.Core.Backend
{
  public static class TextAreaGenerator
  {
    public static TagBuilder Generate(string identity, string value = null, Validation validation = null)
    {
      TagBuilder tb = new TagBuilder(TagNames.TextArea);

      tb.AddCssClass("text-area");
      tb.MergeAttribute(AttributeNames.Id, identity);
      tb.MergeAttribute(AttributeNames.Name, identity);

      if (validation?.IsRequired == true)
        tb.AddRequiredAttributes(validation.IsRequiredValidationErrorMessage);

      if (validation?.MinLength != null || validation?.MaxLength != null)
        tb.AddStringLengthAttributes(validation?.MinLength, validation?.MaxLength, validation.StringLengthValidationErrorMessage);

      if (validation?.IsValid == false)
        tb.AddCssClass("input-validation-error");

      if (!string.IsNullOrEmpty(value))
        tb.InnerHtml.Append(value);

      return tb;
    }
  }
}