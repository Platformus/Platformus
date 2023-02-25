// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Platformus.Core.Backend;

public static class TagBuilderExtensions
{
  public static void AddRequiredAttributes(this TagBuilder tb, string validationErrorMessageText)
  {
    tb.MergeAttribute(AttributeNames.DataVal, true.ToString().ToLower());
    tb.MergeAttribute(AttributeNames.DataValRequired, validationErrorMessageText);
  }

  public static void AddStringLengthAttributes(this TagBuilder tb, int? minLength, int? maxLength, string validationErrorMessageText)
  {
    tb.MergeAttribute(AttributeNames.DataVal, true.ToString().ToLower());
    tb.MergeAttribute(AttributeNames.DataValLength, validationErrorMessageText);

    if (minLength != null)
    {
      tb.MergeAttribute(AttributeNames.DataValLengthMin, minLength.ToString());
      tb.MergeAttribute(AttributeNames.MinLength, maxLength.ToString());
    }

    if (maxLength != null)
    {
      tb.MergeAttribute(AttributeNames.DataValLengthMax, maxLength.ToString());
      tb.MergeAttribute(AttributeNames.MaxLength, maxLength.ToString());
    }
  }
}