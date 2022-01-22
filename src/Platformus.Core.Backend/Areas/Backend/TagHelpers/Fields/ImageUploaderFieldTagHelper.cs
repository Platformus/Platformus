// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Platformus.Core.Backend
{
  public class ImageUploaderFieldTagHelper : FieldTagHelperBase<string>
  {
    public string DestinationBaseUrl { get; set; }
    public int? Width { get; set; }
    public int? Height { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.For == null && string.IsNullOrEmpty(this.Id))
        return;

      base.Process(context, output);
      output.Content.AppendHtml(this.CreateImageUploader());
    }

    private TagBuilder CreateImageUploader()
    {
      IStringLocalizer localizer = this.ViewContext.HttpContext.GetStringLocalizer<ImageUploaderFieldTagHelper>();
      TagBuilder tb = ImageUploaderGenerator.Generate(
        this.GetIdentity(),
        destinationBaseUrl: this.DestinationBaseUrl,
        width: this.Width,
        height: this.Height,
        value: this.GetValue(),
        uploadLabel: localizer["Upload…"],
        removeLabel: localizer["Remove"]
      );

      tb.AddCssClass("field__image-uploader");
      return tb;
    }
  }
}