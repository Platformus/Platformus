// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Platformus.Core.Backend;

public static class ImageUploaderGenerator
{
  public static TagBuilder Generate(string identity, string destinationBaseUrl = null, int? width = null, int? height = null, string value = null, string uploadLabel = "Upload…", string removeLabel = "Remove")
  {
    TagBuilder tb = new TagBuilder(TagNames.Div);

    tb.AddCssClass("image-uploader");
    tb.MergeAttribute("id", identity);

    if (!string.IsNullOrEmpty(destinationBaseUrl))
      tb.MergeAttribute("data-destination-base-url", destinationBaseUrl);

    if (width != null && height != null)
    {
      tb.MergeAttribute("data-width", width.ToString());
      tb.MergeAttribute("data-height", height.ToString());
    }

    tb.InnerHtml.AppendHtml(GenerateImage(value));
    tb.InnerHtml.AppendHtml(GenerateButtons(uploadLabel, removeLabel));
    tb.InnerHtml.AppendHtml(GenerageInput(identity, value));
    return tb;
  }

  private static TagBuilder GenerateImage(string value)
  {
    TagBuilder tb = new TagBuilder(TagNames.Img);

    tb.AddCssClass("image-uploader__image");

    if (string.IsNullOrEmpty(value))
      tb.MergeAttribute(AttributeNames.Style, "display: none;");

    else tb.MergeAttribute(AttributeNames.Src, value);

    return tb;
  }

  private static TagBuilder GenerateButtons(string uploadLabel, string removeLabel)
  {
    TagBuilder tb = new TagBuilder(TagNames.Div);

    tb.AddCssClass("form__buttons form__buttons--minor buttons buttons--minor");
    tb.InnerHtml.AppendHtml(GenerateUploadButton(uploadLabel));
    tb.InnerHtml.AppendHtml(GenerateRemoveButton(removeLabel));
    return tb;
  }

  private static TagBuilder GenerateUploadButton(string uploadLabel)
  {
    TagBuilder tb = new TagBuilder(TagNames.Button);

    tb.AddCssClass("image-uploader__upload-button buttons__button buttons__button--minor button button--neutral button--minor");
    tb.MergeAttribute(AttributeNames.Type, "button");
    tb.InnerHtml.AppendHtml(uploadLabel);
    return tb;
  }

  private static TagBuilder GenerateRemoveButton(string removeLabel)
  {
    TagBuilder tb = new TagBuilder(TagNames.Button);

    tb.AddCssClass("image-uploader__remove-button buttons__button buttons__button--minor button button--negative button--minor button--delete");
    tb.MergeAttribute(AttributeNames.Type, "button");
    tb.InnerHtml.AppendHtml(removeLabel);
    return tb;
  }

  private static TagBuilder GenerageInput(string identity, string value)
  {
    TagBuilder tb = new TagBuilder(TagNames.Input);

    tb.TagRenderMode = TagRenderMode.SelfClosing;
    tb.MergeAttribute(AttributeNames.Name, identity);
    tb.MergeAttribute(AttributeNames.Type, InputTypes.Hidden);

    if (!string.IsNullOrEmpty(value))
      tb.MergeAttribute(AttributeNames.Value, value);

    return tb;
  }
}