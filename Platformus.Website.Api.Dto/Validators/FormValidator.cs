// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;
using Magicalizer.Validators.Abstractions;

namespace Platformus.Website.Api.Dto.Validators;

public class FormValidator : AbstractValidator<Form>
{
  public FormValidator()
  {
    this.RuleFor(f => f.Name).NotEmpty().MaximumLength(64);
    this.When(f => f.Fields != null, () => {
      this.RuleForEach(f => f.Fields!).SetValidator(new FieldValidator(validateForm: false));
    });

    this.RuleSet(RuleSetName.Edit, () => {
      this.RuleFor(f => f.Id).NotEmpty();
    });
  }
}