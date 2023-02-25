// Copyright © 2022 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;

namespace Platformus.Core.Backend;

public static class ValidationAttributeLocalizer
{
  public static void Localize(ValidationAttribute validationAttribute, IStringLocalizer localizer)
  {
    if (string.IsNullOrEmpty(validationAttribute.ErrorMessage) && validationAttribute.ErrorMessageResourceType == null && string.IsNullOrEmpty(validationAttribute.ErrorMessageResourceName))
    {
      if (validationAttribute is RequiredAttribute)
        validationAttribute.ErrorMessage = localizer["Value is required"];

      else if (validationAttribute is StringLengthAttribute stringLengthAttribute)
      {
        if (stringLengthAttribute.MinimumLength == 0)
          validationAttribute.ErrorMessage = localizer["Value must have no more than {1} characters in length"];

        else if (stringLengthAttribute.MaximumLength == 0)
          validationAttribute.ErrorMessage = localizer["Value must have no less than {1} characters in length"];

        else validationAttribute.ErrorMessage = localizer["Value must have from {2} to {1} characters in length"];
      }

      else if (validationAttribute is RangeAttribute)
        validationAttribute.ErrorMessage = localizer["Value must be between {1} and {2}"];

      else if (validationAttribute is RegularExpressionAttribute)
        validationAttribute.ErrorMessage = localizer["Value format is invalid"];

      else if (validationAttribute is CompareAttribute)
        validationAttribute.ErrorMessage = localizer["Values do not match"];
    }
  }
}
