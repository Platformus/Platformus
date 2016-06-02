// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Platformus.Barebone.Backend
{
  public abstract class TagHelperBase : TagHelper
  {
    protected TagBuilder GenerateLabel(ModelExpression modelExpression)
    {
      TagBuilder tb = new TagBuilder("label");

      tb.MergeAttribute("for", this.GetIdentity(modelExpression));
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(modelExpression.Metadata.DisplayName);
      return tb;
    }

    protected TagBuilder GenerateCulture(Localization localization)
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("culture");
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(this.GenerateFlag(localization));
      return tb;
    }

    protected TagBuilder GenerateFlag(Localization localization)
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("flag");
      tb.InnerHtml.Clear();
      tb.InnerHtml.AppendHtml(localization.Culture.Code);
      return tb;
    }

    protected TagBuilder GenerateMultilingualSeparator()
    {
      TagBuilder tb = new TagBuilder("div");

      tb.AddCssClass("multilingual-separator");
      return tb;
    }

    protected string GetIdentity(ModelExpression modelExpression, Localization localization = null)
    {
      string identity = string.Empty;

      if (modelExpression.Name.Length == 1)
        identity = modelExpression.Name[0].ToString().ToLower();

      identity = modelExpression.Name.Remove(1).ToLower() + modelExpression.Name.Substring(1);

      if (localization != null)
        identity += localization.Culture.Code;

      return identity;
    }

    protected string GetValue(ViewContext viewContext, ModelExpression modelExpression, Localization localization = null)
    {
      ModelStateEntry modelState;

      if (viewContext.ModelState.TryGetValue(this.GetIdentity(modelExpression, localization), out modelState) && !string.IsNullOrEmpty(modelState.AttemptedValue))
        return modelState.AttemptedValue;

      if (localization != null)
        return localization.Value;

      return modelExpression.Model == null ? null : modelExpression.Model.ToString();
    }

    protected bool IsValid(ViewContext viewContext, ModelExpression modelExpression, Localization localization = null)
    {
      ModelStateEntry modelState;

      if (viewContext.ModelState.TryGetValue(this.GetIdentity(modelExpression, localization), out modelState))
        return modelState.ValidationState != ModelValidationState.Invalid;

      return true;
    }

    protected void HandleAttributes(TagBuilder tb, ModelExpression modelExpression)
    {
      foreach (object validatorMetadata in modelExpression.Metadata.ValidatorMetadata)
      {
        if (validatorMetadata is RequiredAttribute)
          this.HandleRequiredAttribute(tb, validatorMetadata as RequiredAttribute);

        else if (validatorMetadata is StringLengthAttribute)
          this.HandleStringLengthAttribute(tb, validatorMetadata as StringLengthAttribute);
      }
    }

    protected void HandleRequiredAttribute(TagBuilder tb, RequiredAttribute requiredAttribute)
    {
      tb.AddCssClass("required");
      tb.MergeAttribute("data-val-required", string.Empty);
    }

    protected void HandleStringLengthAttribute(TagBuilder tb, StringLengthAttribute stringLengthAttribute)
    {
      tb.MergeAttribute("data-val-maxlength-max", stringLengthAttribute.MaximumLength.ToString());
      tb.MergeAttribute("maxlength", stringLengthAttribute.MaximumLength.ToString());
    }
  }
}