// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Platformus.Core.Primitives;

namespace Platformus.Core.Backend
{
  public static class ModelExpressionExtensions
  {
    public static string GetLabel(this ModelExpression modelExpression)
    {
      return modelExpression.Metadata.DisplayName;
    }

    public static string GetIdentity(this ModelExpression modelExpression, Localization localization = null)
    {
      string identity = modelExpression.Name;

      if (localization != null)
        identity += localization.Culture.Id;

      return identity.ToCamelCase();
    }

    public static bool HasRequiredAttribute(this ModelExpression modelExpression)
    {
      return GetRequiredAttribute(modelExpression) != null;
    }

    public static bool HasStringLengthAttribute(this ModelExpression modelExpression)
    {
      return GetStringLengthAttribute(modelExpression) != null;
    }

    public static int GetMinStringLength(this ModelExpression modelExpression)
    {
      return GetStringLengthAttribute(modelExpression).MinimumLength;
    }

    public static int GetMaxStringLength(this ModelExpression modelExpression)
    {
      return GetStringLengthAttribute(modelExpression).MaximumLength;
    }

    public static object GetValue(this ModelExpression modelExpression, ViewContext viewContext, Localization localization = null)
    {
      ModelStateEntry modelState;

      if (viewContext.ModelState.TryGetValue(modelExpression.GetIdentity(localization), out modelState))
        if (modelState.AttemptedValue != null)
          return modelState.AttemptedValue;

      if (localization != null)
        return localization.Value;

      return modelExpression.Model;
    }

    public static bool IsValid(this ModelExpression modelExpression, ViewContext viewContext, Localization localization = null)
    {
      ModelStateEntry modelState;

      if (viewContext.ModelState.TryGetValue(modelExpression.GetIdentity(localization), out modelState))
        return modelState.ValidationState != ModelValidationState.Invalid;

      return true;
    }

    private static RequiredAttribute GetRequiredAttribute(ModelExpression modelExpression)
    {
      return modelExpression.Metadata.ValidatorMetadata.FirstOrDefault(vm => vm is RequiredAttribute) as RequiredAttribute;
    }

    private static StringLengthAttribute GetStringLengthAttribute(ModelExpression modelExpression)
    {
      return modelExpression.Metadata.ValidatorMetadata.FirstOrDefault(vm => vm is StringLengthAttribute) as StringLengthAttribute;
    }
  }
}