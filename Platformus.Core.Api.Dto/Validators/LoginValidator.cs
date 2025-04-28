// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;

namespace Platformus.Core.Api.Dto.Validators;

public class LoginValidator : AbstractValidator<Login>
{
  public LoginValidator()
  {
    this.RuleFor(l => l.Email).NotEmpty().MaximumLength(64);
    this.RuleFor(l => l.Password).NotEmpty().MaximumLength(64);
  }
}