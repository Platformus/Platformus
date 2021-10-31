// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Platformus.Core.Backend
{
  public class ImageUploaderFieldTagHelper : TagHelper
  {
    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }
    public string Class { get; set; }
    public ModelExpression For { get; set; }
    public string DestinationBaseUrl { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.For == null)
        return;

      output.TagMode = TagMode.StartTagAndEndTag;
      output.TagName = TagNames.Div;
      output.Attributes.SetAttribute(AttributeNames.Class, "form__field field" + (string.IsNullOrEmpty(this.Class) ? null : $" {this.Class}"));
      output.Content.AppendHtml(this.CreateLabel());
      output.Content.AppendHtml(this.CreateImageUploader());
    }

    private TagBuilder CreateLabel()
    {
      return FieldGenerator.GenerateLabel(this.For.GetLabel(), this.For.GetIdentity());
    }

    private TagBuilder CreateImageUploader()
    {
      IStringLocalizer localizer = this.ViewContext.HttpContext.GetStringLocalizer<ImageUploaderFieldTagHelper>();
      TagBuilder tb = ImageUploaderGenerator.Generate(
        this.For.GetIdentity(),
        this.DestinationBaseUrl,
        this.Width,
        this.Height,
        this.For.GetValue(this.ViewContext)?.ToString(),
        localizer["Upload…"],
        localizer["Remove"]
      );

      tb.AddCssClass("field__image-uploader");
      return tb;
    }
  }
}