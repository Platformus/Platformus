// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Platformus.Barebone.Primitives;

namespace Platformus.Barebone.Backend
{
  public abstract class GeneratorBase
  {
    protected string GetIdentity(ModelExpression modelExpression, Localization localization = null)
    {
      string identity = modelExpression.Name;

      if (identity.Length == 1)
        identity = identity[0].ToString().ToLower();

      else identity = identity.Remove(1).ToLower() + identity.Substring(1);

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

    protected void MergeRequiredAttribute(TagBuilder tb, ModelExpression modelExpression, string requiredCssClass)
    {
      RequiredAttribute requiredAttribute = this.GetRequiredAttribute(modelExpression);

      if (requiredAttribute == null)
        return;

      tb.AddCssClass(requiredCssClass);
      tb.MergeAttribute("data-val", true.ToString().ToLower());
      tb.MergeAttribute("data-val-required", string.Empty);
    }

    protected void MergeStringLengthAttribute(TagBuilder tb, ModelExpression modelExpression)
    {
      StringLengthAttribute stringLengthAttribute = this.GetStringLengthAttribute(modelExpression);

      if (stringLengthAttribute == null)
        return;

      tb.MergeAttribute("data-val", true.ToString().ToLower());
      tb.MergeAttribute("data-val-maxlength-max", stringLengthAttribute.MaximumLength.ToString());
      tb.MergeAttribute("maxlength", stringLengthAttribute.MaximumLength.ToString());
    }

    protected void MergeOtherAttribute(TagBuilder tb, TagHelperAttributeList attributes)
    {
      foreach (TagHelperAttribute attribute in attributes)
        tb.MergeAttribute(attribute.Name, attribute.Value?.ToString());
    }

    protected RequiredAttribute GetRequiredAttribute(ModelExpression modelExpression)
    {
      return modelExpression.Metadata.ValidatorMetadata.FirstOrDefault(vm => vm is RequiredAttribute) as RequiredAttribute;
    }

    protected StringLengthAttribute GetStringLengthAttribute(ModelExpression modelExpression)
    {
      return modelExpression.Metadata.ValidatorMetadata.FirstOrDefault(vm => vm is StringLengthAttribute) as StringLengthAttribute;
    }
  }
}