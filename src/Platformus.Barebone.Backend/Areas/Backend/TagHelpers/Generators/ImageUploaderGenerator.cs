// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Barebone.Backend
{
  public class ImageUploaderGenerator : GeneratorBase
  {
    public TagBuilder GenerateImageUploader(ViewContext viewContext, ModelExpression modelExpression, TagHelperAttributeList attributes, int? width, int? height, string additionalCssClass = null)
    {
      TagBuilder tb = new TagBuilder("div");

      if (!string.IsNullOrEmpty(additionalCssClass))
        tb.AddCssClass(additionalCssClass);

      tb.AddCssClass("image-uploader");
      tb.MergeAttribute("id", this.GetIdentity(modelExpression));

      if (width != null && height != null)
      {
        tb.MergeAttribute("data-width", width.ToString());
        tb.MergeAttribute("data-height", height.ToString());
      }

      this.MergeOtherAttribute(tb, attributes);
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(this.GenerageInput(viewContext, modelExpression));
      tb.InnerHtml.AppendHtml(this.GenerateImage(viewContext, modelExpression));
      tb.InnerHtml.AppendHtml(this.GenerateButtons());
      return tb;
    }

    private TagBuilder GenerageInput(ViewContext viewContext, ModelExpression modelExpression)
    {
      TagBuilder tb = new TagBuilder("input");

      tb.TagRenderMode = TagRenderMode.SelfClosing;
      tb.MergeAttribute("name", this.GetIdentity(modelExpression));
      tb.MergeAttribute("type", "hidden");

      string value = this.GetValue(viewContext, modelExpression);

      if (!string.IsNullOrEmpty(value))
        tb.MergeAttribute("value", value);

      return tb;
    }

    private TagBuilder GenerateImage(ViewContext viewContext, ModelExpression modelExpression)
    {
      TagBuilder tb = new TagBuilder("img");

      tb.AddCssClass("image-uploader__image");

      string value = this.GetValue(viewContext, modelExpression);

      if (string.IsNullOrEmpty(value))
        tb.MergeAttribute("style", "display: none;");

      else tb.MergeAttribute("src", value);

      return tb;
    }

    private TagBuilder GenerateButtons()
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("form__buttons form__buttons--minor buttons");
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(this.GenerateUploadButton());
      tb.InnerHtml.AppendHtml(this.GenerateRemoveButton());
      return tb;
    }

    private TagBuilder GenerateUploadButton()
    {
      TagBuilder tb = new TagBuilder("button");

      tb.AddCssClass("image-uploader__upload-button buttons__button buttons__button--minor button button--positive button--minor");
      tb.MergeAttribute("type", "button");
      tb.InnerHtml.AppendHtml("Upload…");
      return tb;
    }

    private TagBuilder GenerateRemoveButton()
    {
      TagBuilder tb = new TagBuilder("button");

      tb.AddCssClass("image-uploader__remove-button buttons__button buttons__button--minor button button--negative button--minor");
      tb.MergeAttribute("type", "button");
      tb.InnerHtml.AppendHtml("Remove");
      return tb;
    }
  }
}