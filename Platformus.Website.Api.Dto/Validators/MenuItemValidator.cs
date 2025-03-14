// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;
using Magicalizer.Validators.Abstractions;

namespace Platformus.Website.Api.Dto.Validators;

public class MenuItemValidator : AbstractValidator<MenuItem>
{
  public MenuItemValidator(bool validateMenu = true)
  {
    if (validateMenu)
      this.RuleFor(mi => mi.Menu).NotNull().DependentRules(() => {
        this.RuleFor(mi => mi.Menu!.Id).GreaterThan(0);
      });

    this.When(mi => mi.LocalizedMenuItems != null, () => {
      this.RuleForEach(mi => mi.LocalizedMenuItems!).SetValidator(new LocalizedMenuItemValidator(validateMenuItem: false));
    });

    this.RuleSet(RuleSetName.Edit, () => {
      this.RuleFor(mi => mi.Id).NotEmpty();
    });
  }
}