// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;
using Magicalizer.Validators.Abstractions;

namespace Platformus.Website.Api.Dto.Validators;

public class FieldValidator : AbstractValidator<Field>
{
  public FieldValidator(bool validateForm = true)
  {
    if (validateForm)
      this.RuleFor(f => f.Form).NotNull().DependentRules(() => {
        this.RuleFor(f => f.Form!.Id).GreaterThan(0);
      });

    this.RuleFor(f => f.FieldType).NotNull().DependentRules(() => {
      this.RuleFor(f => f.FieldType!.Id).NotEmpty();
    });

    this.When(f => f.LocalizedFields != null, () => {
      this.RuleForEach(f => f.LocalizedFields!).SetValidator(new LocalizedFieldValidator(validateField: false));
    });

    this.RuleSet(RuleSetName.Edit, () => {
      this.RuleFor(f => f.Id).NotEmpty();
    });
  }
}