// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;

namespace Platformus.Website.Api.Dto.Validators;

public class LocalizedPropertyValidator : AbstractValidator<LocalizedProperty>
{
  public LocalizedPropertyValidator(bool validateProperty = true)
  {
    if (validateProperty)
      this.RuleFor(lp => lp.Property).NotNull().DependentRules(() => {
        this.RuleFor(lp => lp.Property!.Id).GreaterThan(0);
      });

    this.RuleFor(lp => lp.Culture).NotNull().DependentRules(() => {
      this.RuleFor(lp => lp.Culture!.Id).NotEmpty().Length(2, 2);
    });
  }
}