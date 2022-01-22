// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  public class ColumnTagHelper : TagHelper
  {
    public string Label { get; set; }
    public string SortingPropertyPath { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      TableTagHelper.Column column = new TableTagHelper.Column(this.Label, this.SortingPropertyPath);

      (context.Items["Columns"] as List<TableTagHelper.Column>).Add(column);
      output.SuppressOutput();
    }
  }
}