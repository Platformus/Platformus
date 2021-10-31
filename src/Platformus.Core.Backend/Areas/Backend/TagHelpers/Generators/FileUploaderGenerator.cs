// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Platformus.Core.Backend
{
  public static class FileUploaderGenerator
  {
    public static TagBuilder Generate(string identity, bool isMultiple, string fileNotSelectedLabel = "File not selected", string browseLabel = "Browse…")
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("file-uploader");
      tb.MergeAttribute(AttributeNames.Id, identity);
      tb.InnerHtml.AppendHtml(GenerateFilename(fileNotSelectedLabel));
      tb.InnerHtml.AppendHtml(GenerateButtons(identity, isMultiple, browseLabel));
      return tb;
    }

    private static TagBuilder GenerateFilename(string fileNotSelectedLabel)
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("file-uploader__filename file-uploader__filename--not-selected");
      tb.InnerHtml.AppendHtml(fileNotSelectedLabel);
      return tb;
    }

    private static TagBuilder GenerateButtons(string identity, bool isMultiple, string browseLabel)
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("form__buttons form__buttons--minor buttons");
      tb.InnerHtml.AppendHtml(GenerateBrowseButton(identity, isMultiple, browseLabel));
      return tb;
    }

    private static TagBuilder GenerateBrowseButton(string identity, bool isMultiple, string browseLabel)
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("file-uploader__browse-button buttons__button button button--positive button--minor");
      tb.InnerHtml.AppendHtml(browseLabel);
      tb.InnerHtml.AppendHtml(GenerateBrowseInput(identity, isMultiple));
      return tb;
    }

    private static TagBuilder GenerateBrowseInput(string identity, bool isMultiple)
    {
      TagBuilder tb = new TagBuilder(TagNames.Input);

      tb.AddCssClass("file-uploader__browse-input");
      tb.MergeAttribute(AttributeNames.Name, identity);
      tb.MergeAttribute(AttributeNames.Type, "file");
      tb.MergeAttribute("size", "1");

      if (isMultiple)
        tb.MergeAttribute("multiple", string.Empty);

      return tb;
    }
  }
}