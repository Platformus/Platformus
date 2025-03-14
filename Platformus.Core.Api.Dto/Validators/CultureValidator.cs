// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;

namespace Platformus.Core.Api.Dto.Validators;

public class CultureValidator : AbstractValidator<Culture>
{
  public CultureValidator()
  {
    this.RuleFor(c => c.Id).NotEmpty().Length(2, 2);
    this.RuleFor(c => c.Name).NotEmpty().MaximumLength(64);
  }
}