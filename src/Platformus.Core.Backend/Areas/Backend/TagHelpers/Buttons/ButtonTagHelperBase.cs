// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  public abstract class ButtonTagHelperBase : TagHelper
  {
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }
    public string Class { get; set; }
    public bool IsMinor { get; set; }
    public string Href { get; set; }
    public bool DoNotCombineUrl { get; set; }
    public string SkipUrlParameters { get; set; }

    [HtmlAttributeName(AttributeNames.OnClick)]
    public string OnClick { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (!string.IsNullOrEmpty(this.Href))
        output.TagName = TagNames.A;

      else output.TagName = TagNames.Button;

      output.TagMode = TagMode.StartTagAndEndTag;
      output.Attributes.SetAttribute(AttributeNames.Class, this.GetClass());

      if (string.IsNullOrEmpty(this.Href))
        output.Attributes.SetAttribute(AttributeNames.Type, "button");

      else output.Attributes.SetAttribute(AttributeNames.Href, this.GetHref());

      output.Attributes.SetAttribute(AttributeNames.OnClick, this.OnClick);
    }

    protected virtual string GetClass()
    {
      if (this.IsMinor)
        return $"buttons__button buttons__button--minor button button--minor" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}");

      return $"buttons__button button" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}");
    }

    private string GetHref()
    {
      if (this.DoNotCombineUrl)
        return this.Href;

      return this.ViewContext.HttpContext.Request.CombineUrl(this.Href, this.CreateUrlParameters());
    }

    private Url.Parameter[] CreateUrlParameters()
    {
      if (string.IsNullOrEmpty(this.SkipUrlParameters))
        return Array.Empty<Url.Parameter>();

      return this.SkipUrlParameters.Split(',').Select(p => new Url.Parameter(name: p, skip: true)).ToArray();
    }
  }
}