// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  public class DeleteButtonTagHelper : TagHelper
  {
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }
    public string Class { get; set; }
    public string Href { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      output.TagName = TagNames.Button;
      output.Attributes.SetAttribute(AttributeNames.Class, "buttons__button buttons__button--minor button button--negative button--minor button--delete" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));

      string href = Url.Combine(
        this.ViewContext.HttpContext.Request,
        this.Href,
        new Url.Descriptor(name: "skip", skip: true),
        new Url.Descriptor(name: "take", skip: true)
      );
      
      output.Attributes.SetAttribute(AttributeNames.OnClick, $"return platformus.forms.deleteForm.show('{href}');");
    }
  }
}