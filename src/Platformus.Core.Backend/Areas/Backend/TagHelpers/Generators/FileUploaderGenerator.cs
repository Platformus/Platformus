// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;

namespace Platformus.Core.Backend
{
  public static class FileUploaderGenerator
  {
    public static TagBuilder Generate(string identity, bool isMultiple)
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("file-uploader");
      tb.MergeAttribute(AttributeNames.Id, identity);
      tb.InnerHtml.AppendHtml(GenerateFilename());
      tb.InnerHtml.AppendHtml(GenerateButtons(identity, isMultiple));
      return tb;
    }

    private static TagBuilder GenerateFilename()
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("file-uploader__filename file-uploader__filename--not-selected");
      tb.InnerHtml.AppendHtml("File not selected");
      return tb;
    }

    private static TagBuilder GenerateButtons(string identity, bool isMultiple)
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("form__buttons form__buttons--minor buttons");
      tb.InnerHtml.AppendHtml(GenerateBrowseButton(identity, isMultiple));
      return tb;
    }

    private static TagBuilder GenerateBrowseButton(string identity, bool isMultiple)
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("file-uploader__browse-button buttons__button button button--positive button--minor");
      tb.InnerHtml.AppendHtml("Browse…");
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