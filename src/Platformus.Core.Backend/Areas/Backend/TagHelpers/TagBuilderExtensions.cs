// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Platformus.Core.Backend
{
  public static class TagBuilderExtensions
  {
    public static void AddRequiredAttributes(this TagBuilder tb, string className)
    {
      tb.AddCssClass(className);
      tb.MergeAttribute(AttributeNames.DataVal, true.ToString().ToLower());
      tb.MergeAttribute(AttributeNames.DataValRequired, string.Empty);
    }

    public static void AddMaxLengthAttributes(this TagBuilder tb, int maxLength)
    {
      tb.MergeAttribute(AttributeNames.DataVal, true.ToString().ToLower());
      tb.MergeAttribute(AttributeNames.DataValMaxLengthMax, maxLength.ToString());
      tb.MergeAttribute(AttributeNames.MaxLength, maxLength.ToString());
    }
  }
}