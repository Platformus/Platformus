// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  public class PositiveButtonTagHelper : TagHelper
  {
    public string Class { get; set; }
    public bool IsMinor { get; set; }
    public string Href { get; set; }
    public string Onclick { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (!string.IsNullOrEmpty(this.Href))
        output.TagName = TagNames.A;

      else output.TagName = TagNames.Button;
      
      if (this.IsMinor)
        output.Attributes.SetAttribute(AttributeNames.Class, "buttons__button buttons__button--minor button button--positive button--minor" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));

      else output.Attributes.SetAttribute(AttributeNames.Class, "buttons__button button button--positive" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));

      if (string.IsNullOrEmpty(this.Href))
        output.Attributes.SetAttribute(AttributeNames.Type, "button");

      output.Attributes.SetAttribute(AttributeNames.Href, this.Href);
      output.Attributes.SetAttribute(AttributeNames.OnClick, this.Onclick);
    }
  }
}