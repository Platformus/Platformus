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
      tb.InnerHtml.AppendHtml(GenerateButtons(identity, browseLabel));
      tb.InnerHtml.AppendHtml(GenerateInput(identity, isMultiple));
      return tb;
    }

    private static TagBuilder GenerateFilename(string fileNotSelectedLabel)
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("file-uploader__filename file-uploader__filename--not-selected");
      tb.InnerHtml.AppendHtml(fileNotSelectedLabel);
      return tb;
    }

    private static TagBuilder GenerateButtons(string identity, string browseLabel)
    {
      TagBuilder tb = new TagBuilder(TagNames.Div);

      tb.AddCssClass("form__buttons form__buttons--minor buttons buttons--minor");
      tb.InnerHtml.AppendHtml(GenerateBrowseButton(identity, browseLabel));
      return tb;
    }

    private static TagBuilder GenerateBrowseButton(string identity, string browseLabel)
    {
      TagBuilder tb = new TagBuilder(TagNames.Label);

      tb.AddCssClass("buttons__button button button--neutral button--minor button--icon icon icon--browse");
      tb.MergeAttribute(AttributeNames.For, $"{identity}FileInput");
      tb.InnerHtml.AppendHtml(browseLabel);
      return tb;
    }

    private static TagBuilder GenerateInput(string identity, bool isMultiple)
    {
      TagBuilder tb = new TagBuilder(TagNames.Input);

      tb.AddCssClass("file-uploader__input");
      tb.MergeAttribute(AttributeNames.Id, $"{identity}FileInput");
      tb.MergeAttribute(AttributeNames.Name, identity);
      tb.MergeAttribute(AttributeNames.Type, InputTypes.File);

      if (isMultiple)
        tb.MergeAttribute("multiple", string.Empty);

      return tb;
    }
  }
}