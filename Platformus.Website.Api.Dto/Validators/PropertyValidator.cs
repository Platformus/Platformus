// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;
using Magicalizer.Validators.Abstractions;

namespace Platformus.Website.Api.Dto.Validators;

public class PropertyValidator : AbstractValidator<Property>
{
  public PropertyValidator(bool validateObject = true)
  {
    if (validateObject)
      this.RuleFor(p => p.Object).NotNull().DependentRules(() => {
        this.RuleFor(p => p.Object!.Id).GreaterThan(0);
      });

    this.RuleFor(p => p.Member).NotNull().DependentRules(() => {
      this.RuleFor(p => p.Member!.Id).GreaterThan(0);
    });

    this.When(p => p.LocalizedProperties != null, () => {
      this.RuleForEach(p => p.LocalizedProperties!).SetValidator(new LocalizedPropertyValidator(validateProperty: false));
    });

    if (validateObject)
      this.RuleSet(RuleSetName.Edit, () => {
        this.RuleFor(p => p.Id).NotEmpty();
      });
  }
}