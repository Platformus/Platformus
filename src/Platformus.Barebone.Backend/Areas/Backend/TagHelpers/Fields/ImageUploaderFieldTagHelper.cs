// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Barebone.Backend
{
  [HtmlTargetElement("image-uploader-field", Attributes = ForAttributeName)]
  public class ImageUploaderFieldTagHelper : TagHelper
  {
    private const string ForAttributeName = "asp-for";
    private const string WidthAttributeName = "asp-width";
    private const string HeightAttributeName = "asp-height";

    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }

    [HtmlAttributeName(ForAttributeName)] 
    public ModelExpression For { get; set; }

    [HtmlAttributeName(WidthAttributeName)]
    public int? Width { get; set; }

    [HtmlAttributeName(HeightAttributeName)]
    public int? Height { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.For == null)
        return;

      output.SuppressOutput();
      output.Content.Clear();
      output.Content.AppendHtml(this.GenerateField(output.Attributes));
    }

    private TagBuilder GenerateField(TagHelperAttributeList attributes)
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("form__field field");
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(new FieldGenerator().GenerateLabel(this.For));
      tb.InnerHtml.AppendHtml(new ImageUploaderGenerator().GenerateImageUploader(this.ViewContext, this.For, attributes, this.Width, this.Height, "field__image-uploader"));
      return tb;
    }
  }
}