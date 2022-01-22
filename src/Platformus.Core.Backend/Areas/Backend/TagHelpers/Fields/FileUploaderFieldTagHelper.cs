// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;

namespace Platformus.Core.Backend
{
  public class FileUploaderFieldTagHelper : FieldTagHelperBase<string>
  {
    public bool IsMultiple { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      if (this.For == null && string.IsNullOrEmpty(this.Id))
        return;

      base.Process(context, output);
      output.Content.AppendHtml(this.CreateFileUploader());
    }

    private TagBuilder CreateFileUploader()
    {
      IStringLocalizer localizer = this.ViewContext.HttpContext.GetStringLocalizer<FileUploaderFieldTagHelper>();
      TagBuilder tb = FileUploaderGenerator.Generate(
        this.GetIdentity(),
        this.IsMultiple,
        fileNotSelectedLabel: localizer["File not selected"],
        browseLabel: localizer["Browse…"]
      );

      tb.AddCssClass("field__file-uploader");

      // TODO: merge all the attributes, not only "onchange"
      if (!string.IsNullOrEmpty(this.OnChange))
        tb.MergeAttribute(AttributeNames.OnChange, this.OnChange);

      return tb;
    }
  }
}