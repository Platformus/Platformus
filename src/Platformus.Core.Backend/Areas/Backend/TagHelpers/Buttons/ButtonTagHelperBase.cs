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
    public string Onclick { get; set; }

    protected string CssClassModifier { get; }

    public ButtonTagHelperBase(string cssClassModifier)
    {
      this.CssClassModifier = cssClassModifier;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (!string.IsNullOrEmpty(this.Href))
        output.TagName = TagNames.A;

      else output.TagName = TagNames.Button;
      
      if (this.IsMinor)
        output.Attributes.SetAttribute(AttributeNames.Class, $"buttons__button buttons__button--minor button button--{this.CssClassModifier} button--minor" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));

      else output.Attributes.SetAttribute(AttributeNames.Class, $"buttons__button button button--{this.CssClassModifier}" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));

      if (string.IsNullOrEmpty(this.Href))
        output.Attributes.SetAttribute(AttributeNames.Type, "button");

      else
      {
        string href;

        if (this.DoNotCombineUrl)
          href = this.Href;

        else href = this.ViewContext.HttpContext.Request.CombineUrl(this.Href, this.CreateUrlDescriptors());

        output.Attributes.SetAttribute(AttributeNames.Href, href);
      }

      output.Attributes.SetAttribute(AttributeNames.OnClick, this.Onclick);
    }

    private Url.Descriptor[] CreateUrlDescriptors()
    {
      if (string.IsNullOrEmpty(this.SkipUrlParameters))
        return Array.Empty<Url.Descriptor>();

      return this.SkipUrlParameters.Split(',').Select(p => new Url.Descriptor(name: p, skip: true)).ToArray();
    }
  }
}