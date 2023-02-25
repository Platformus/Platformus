// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend;

public class FilterLabelTagHelper : TagHelper
{
  public override void Process(TagHelperContext context, TagHelperOutput output)
  {
    output.TagName = TagNames.Label;
    output.Attributes.SetAttribute(AttributeNames.Class, "filter__label");
  }
}