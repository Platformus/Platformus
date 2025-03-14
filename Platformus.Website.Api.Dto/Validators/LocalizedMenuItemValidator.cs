// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;

namespace Platformus.Website.Api.Dto.Validators;

public class LocalizedMenuItemValidator : AbstractValidator<LocalizedMenuItem>
{
  public LocalizedMenuItemValidator(bool validateMenuItem = true)
  {
    if (validateMenuItem)
      this.RuleFor(lmi => lmi.MenuItem).NotNull().DependentRules(() => {
        this.RuleFor(lmi => lmi.MenuItem!.Id).GreaterThan(0);
      });

    this.RuleFor(lmi => lmi.Culture).NotNull().DependentRules(() => {
      this.RuleFor(lmi => lmi.Culture!.Id).NotEmpty().Length(2, 2);
    });

    this.RuleFor(lmi => lmi.Name).NotEmpty().MaximumLength(64);
  }
}