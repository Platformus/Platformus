﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  [HtmlTargetElement("password-box", Attributes = ForAttributeName)]
  public class PasswordBoxTagHelper : TagHelper
  {
    private const string ForAttributeName = "asp-for";

    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }

    [HtmlAttributeName(ForAttributeName)] 
    public ModelExpression For { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.ViewContext == null || this.For == null)
        return;

      output.SuppressOutput();
      output.Content.Clear();
      output.Content.AppendHtml(new TextBoxGenerator().GenerateTextBox(this.ViewContext, this.For, output.Attributes, null, "password"));
    }
  }
}