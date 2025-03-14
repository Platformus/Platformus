// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;
using Magicalizer.Validators.Abstractions;

namespace Platformus.Website.Api.Dto.Validators;

public class ObjectValidator : AbstractValidator<Object>
{
  public ObjectValidator()
  {
    this.RuleFor(o => o.Class).NotNull().DependentRules(() => {
      this.RuleFor(o => o.Class!.Id).GreaterThan(0);
    });

    this.When(o => o.Properties != null, () => {
      this.RuleForEach(o => o.Properties!).SetValidator(new PropertyValidator(validateObject: false));
    });

    this.RuleSet(RuleSetName.Edit, () => {
      this.RuleFor(o => o.Id).NotEmpty();
    });
  }
}