// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Core.Backend
{
  public class ImageFieldTagHelper : FieldTagHelperBase<string>
  {
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.For == null && string.IsNullOrEmpty(this.Id))
        return;

      base.Process(context, output);
      output.Content.AppendHtml(this.CreateImageView());
    }

    private TagBuilder CreateImageView()
    {
      TagBuilder tb = ImageViewGenerator.Generate(
        this.GetIdentity(),
        this.GetValue()
      );

      tb.AddCssClass("field__image-view");
      return tb;
    }
  }
}