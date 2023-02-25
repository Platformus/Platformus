// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Platformus.Core;

public class Validation
{
  public bool IsRequired { get; }
  public string IsRequiredValidationErrorMessage { get; }
  public int? MinLength { get; }
  public int? MaxLength { get; }
  public string StringLengthValidationErrorMessage { get; }
  public bool IsValid { get; }

  public Validation(bool isRequired, string isRequiredValidationErrorMessage, int? minLength, int? maxLength, string stringLengthValidationErrorMessage, bool isValid)
  {
    this.IsRequired = isRequired;
    this.IsRequiredValidationErrorMessage = isRequiredValidationErrorMessage;
    this.MinLength = minLength;
    this.MaxLength = maxLength;
    this.StringLengthValidationErrorMessage = stringLengthValidationErrorMessage;
    this.IsValid = isValid;
  }
}