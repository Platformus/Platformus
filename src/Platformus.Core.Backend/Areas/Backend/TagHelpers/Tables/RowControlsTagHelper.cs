// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend;

[RestrictChildren("neutral-button", "positive-button", "negative-button", "delete-button", "partial")]
public class RowControlsTagHelper : TagHelper
{
  public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
  {
    TagHelperContent content = await output.GetChildContentAsync();

    output.SuppressOutput();

    RowTagHelper.RowControls rowControls = context.Items["RowControls"] as RowTagHelper.RowControls;

    rowControls.Content = content;
  }
}