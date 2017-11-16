// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Barebone.Backend
{
  public class FileUploaderGenerator : GeneratorBase
  {
    public TagBuilder GenerateFileUploader(ViewContext viewContext, ModelExpression modelExpression, TagHelperAttributeList attributes, bool isMultiple, string additionalCssClass = null)
    {
      TagBuilder tb = new TagBuilder("div");

      if (!string.IsNullOrEmpty(additionalCssClass))
        tb.AddCssClass(additionalCssClass);

      tb.AddCssClass("file-uploader");
      tb.MergeAttribute("id", this.GetIdentity(modelExpression));
      this.MergeOtherAttribute(tb, attributes);
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(this.GenerateFilename());
      tb.InnerHtml.AppendHtml(this.GenerateButtons(modelExpression, isMultiple));
      return tb;
    }

    private TagBuilder GenerateFilename()
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("file-uploader__filename file-uploader__filename--not-selected");
      tb.InnerHtml.AppendHtml("File not selected");
      return tb;
    }

    private TagBuilder GenerateButtons(ModelExpression modelExpression, bool isMultiple)
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("form__buttons form__buttons--minor buttons");
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(this.GenerateBrowseButton(modelExpression, isMultiple));
      return tb;
    }

    private TagBuilder GenerateBrowseButton(ModelExpression modelExpression, bool isMultiple)
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("file-uploader__browse-button buttons__button button button--positive button--minor");
      tb.InnerHtml.AppendHtml("Browse…");
      tb.InnerHtml.AppendHtml(this.GenerateBrowseInput(modelExpression, isMultiple));
      return tb;
    }

    private TagBuilder GenerateBrowseInput(ModelExpression modelExpression, bool isMultiple)
    {
      TagBuilder tb = new TagBuilder("input");

      tb.AddCssClass("file-uploader__browse-input");
      tb.MergeAttribute("name", this.GetIdentity(modelExpression));
      tb.MergeAttribute("type", "file");
      tb.MergeAttribute("size", "1");

      if (isMultiple)
        tb.MergeAttribute("multiple", string.Empty);

      return tb;
    }
  }
}