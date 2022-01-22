// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Platformus.Core.Backend;

namespace Platformus.Core
{
  public static class ValidationFactory
  {
    public static Validation Create(ViewContext viewContext, bool isRequired = false, int? minLength = null, int? maxLength = null, bool isValid = false)
    {
      IStringLocalizer localizer = viewContext.HttpContext.GetStringLocalizer<SharedResource>();

      return new Validation(
        isRequired,
        isRequired ? localizer["Value is required"] : null,
        minLength,
        maxLength,
        minLength != null || maxLength != null ? GetStringLengthValidationErrorMessage(localizer, minLength, maxLength) : null,
        isValid
      );
    }

    private static string GetStringLengthValidationErrorMessage(IStringLocalizer localizer, int? minLength, int? maxLength)
    {
      string validationErrorMessage;

      if (minLength == null)
        validationErrorMessage = localizer["Value must have no more than {1} characters in length"];

      else if (maxLength == null)
        validationErrorMessage = localizer["Value must have no less than {1} characters in length"];

      else validationErrorMessage = Swap1And2ParameterIndices(localizer["Value must have from {2} to {1} characters in length"]);

      return ShiftParameterIndices(validationErrorMessage);
    }

    private static string ShiftParameterIndices(string value)
    {
      // TODO: looks ugly
      return value.Replace("{1}", "{0}").Replace("{2}", "{1}").Replace("{3}", "{2}");
    }

    private static string Swap1And2ParameterIndices(string value)
    {
      // TODO: looks even more ugly
      return value.Replace("{1}", "{X}").Replace("{2}", "{1}").Replace("{X}", "{2}");
    }
  }
}