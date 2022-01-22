// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend
{
  public abstract class FieldTagHelperBase<T> : EmptyFieldTagHelperBase<T>
  {
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
      base.Process(context, output);
      output.Content.AppendHtml(this.CreateLabel());

      if (this.IsRequired())
        output.Content.AppendHtml(this.CreateRequiredMarker());
    }

    protected TagBuilder CreateValidationErrorMessage(Localization localization = null)
    {
      return ValidationErrorMessageGenerator.Generate(
        this.GetIdentity(localization),
        this.IsValid(localization),
        this.GetValidationErrors(localization)?.FirstOrDefault()
      );
    }

    private TagBuilder CreateLabel()
    {
      return FieldGenerator.GenerateLabel(this.GetLabel(), this.GetIdentity());
    }

    private TagBuilder CreateRequiredMarker()
    {
      IStringLocalizer localizer = this.ViewContext.HttpContext.GetStringLocalizer<SharedResource>();

      return FieldGenerator.GenerateRequiredMarker(localizer["required"]);
    }
  }
}