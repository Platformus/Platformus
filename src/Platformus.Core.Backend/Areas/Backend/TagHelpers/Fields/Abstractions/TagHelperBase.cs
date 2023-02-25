// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend;

public abstract class TagHelperBase<T> : TagHelper
{
  [HtmlAttributeNotBound]
  [ViewContext]
  public ViewContext ViewContext { get; set; }
  public string Class { get; set; }
  public ModelExpression For { get; set; }
  public T Value { get; set; }
  public string Id { get; set; }
  public string Label { get; set; }
  public bool Disabled { get; set; }
  public bool Required { get; set; }

  [HtmlAttributeName(AttributeNames.OnChange)]
  public string OnChange { get; set; }

  protected string GetValue(Localization localization = null)
  {
    if (this.ViewContext.ModelState.TryGetValue(this.GetIdentity(localization), out ModelStateEntry modelState))
      if (modelState.AttemptedValue != null)
        return modelState.AttemptedValue;

    if (localization != null)
      return localization.Value;

    return this.FormatValue(this.For == null ? this.Value : this.For.Model);
  }

  protected virtual string FormatValue(object value)
  {
    return value?.ToString();
  }

  protected string GetIdentity(Localization localization = null)
  {
    return ((this.For == null ? this.Id : this.For.Name) + localization?.Culture.Id).ToCamelCase();
  }

  protected string GetLabel()
  {
    return this.For == null ? this.Label : this.For.GetLabel();
  }

  protected bool IsDisabled()
  {
    return this.Disabled;
  }

  protected bool IsRequired()
  {
    return this.For == null ? this.Required : this.For.HasRequiredAttribute();
  }

  protected bool IsValid(Localization localization = null)
  {
    if (this.ViewContext.ModelState.TryGetValue(this.GetIdentity(localization), out ModelStateEntry modelState))
      return modelState.ValidationState != ModelValidationState.Invalid;

    return true;
  }

  protected IEnumerable<string> GetValidationErrors(Localization localization = null)
  {
    if (this.ViewContext.ModelState.TryGetValue(this.GetIdentity(localization), out ModelStateEntry modelState))
      if (modelState.ValidationState == ModelValidationState.Invalid)
        return modelState.Errors.Select(e => e.ErrorMessage);

    return null;
  }

  protected virtual Validation GetValidation()
  {
    return ValidationFactory.Create(this.ViewContext, isRequired: this.IsRequired(), isValid: this.IsValid());
  }
}