// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;

namespace Platformus.Website.Api.Dto.Validators;

public class LocalizedFieldValidator : AbstractValidator<LocalizedField>
{
  public LocalizedFieldValidator(bool validateField = true)
  {
    if (validateField)
      this.RuleFor(lf => lf.Field).NotNull().DependentRules(() => {
        this.RuleFor(lf => lf.Field!.Id).GreaterThan(0);
      });

    this.RuleFor(lf => lf.Culture).NotNull().DependentRules(() => {
      this.RuleFor(lf => lf.Culture!.Id).NotEmpty().Length(2, 2);
    });

    this.RuleFor(lf => lf.Name).NotEmpty().MaximumLength(64);
  }
}