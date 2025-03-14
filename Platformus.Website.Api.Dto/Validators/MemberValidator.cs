// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;
using Magicalizer.Validators.Abstractions;

namespace Platformus.Website.Api.Dto.Validators;

public class MemberValidator : AbstractValidator<Member>
{
  public MemberValidator(bool validateClass = true)
  {
    if (validateClass)
      this.RuleFor(m => m.Class).NotNull().DependentRules(() => {
        this.RuleFor(m => m.Class!.Id).GreaterThan(0);
      });

    this.RuleFor(m => m.MemberType).NotNull().DependentRules(() => {
      this.RuleFor(m => m.MemberType!.Id).NotEmpty();
    });

    this.RuleFor(m => m.Name).NotEmpty().MaximumLength(64);
    this.RuleFor(m => m.CSharpName).MaximumLength(32);

    this.RuleSet(RuleSetName.Edit, () => {
      this.RuleFor(m => m.Id).NotEmpty();
    });
  }
}