// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Platformus.Core.Backend
{
  public class DeleteButtonTagHelper : TagHelper
  {
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }
    public string Class { get; set; }
    public string Href { get; set; }

    [HtmlAttributeName(AttributeNames.OnClick)]
    public string OnClick { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      output.TagName = TagNames.Button;
      output.TagMode = TagMode.StartTagAndEndTag;
      output.Attributes.SetAttribute(AttributeNames.Class, "buttons__button buttons__button--minor button button--negative button--minor button--delete" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));

      if (!string.IsNullOrEmpty(this.Href))
      {
        string href = Url.Combine(
          this.ViewContext.HttpContext.Request,
          this.Href,
          new Url.Parameter(name: "skip", skip: true),
          new Url.Parameter(name: "take", skip: true)
        );

        output.Attributes.SetAttribute(AttributeNames.OnClick, $"event.stopPropagation(); return platformus.forms.deleteForm.show('{href}');");
      }

      else if (!string.IsNullOrEmpty(this.OnClick))
      {
        if (this.OnClick.EndsWith(';'))
          this.OnClick = this.OnClick.Remove(this.OnClick.Length - 1); // TODO: looks ugly

        output.Attributes.SetAttribute(AttributeNames.OnClick, $"event.stopPropagation(); {this.OnClick};");
      }

      IStringLocalizer localizer = this.ViewContext.HttpContext.GetStringLocalizer<DeleteButtonTagHelper>();

      output.Content.SetHtmlContent(localizer["Delete"]);
    }
  }
}