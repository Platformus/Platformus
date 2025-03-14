// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;
using Magicalizer.Validators.Abstractions;

namespace Platformus.Website.Api.Dto.Validators;

public class MenuValidator : AbstractValidator<Menu>
{
  public MenuValidator()
  {
    this.RuleFor(m => m.Name).NotEmpty().MaximumLength(64);
    this.When(m => m.MenuItems != null, () => {
      this.RuleForEach(m => m.MenuItems!).SetValidator(new MenuItemValidator(validateMenu: false));
    });

    this.RuleSet(RuleSetName.Edit, () => {
      this.RuleFor(m => m.Id).NotEmpty();
    });
  }
}