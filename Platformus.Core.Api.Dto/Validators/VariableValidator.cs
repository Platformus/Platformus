// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;
using Magicalizer.Validators.Abstractions;

namespace Platformus.Core.Api.Dto.Validators;

public class VariableValidator : AbstractValidator<Variable>
{
  public VariableValidator()
  {
    this.RuleFor(v => v.Code).NotEmpty().MaximumLength(32);
    this.RuleFor(v => v.Name).NotEmpty().MaximumLength(64);
    this.RuleFor(v => v.Value).MaximumLength(1024);

    this.RuleSet(RuleSetName.Create, () => {
      this.RuleFor(v => v.Configuration).NotNull().DependentRules(() => {
        this.RuleFor(v => v.Configuration!.Id).GreaterThan(0);
      });
    });

    this.RuleSet(RuleSetName.Edit, () => {
      this.RuleFor(v => v.Id).NotEmpty();
    });
  }
}