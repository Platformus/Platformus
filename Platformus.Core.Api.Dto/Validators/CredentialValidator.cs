// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;
using Magicalizer.Validators.Abstractions;

namespace Platformus.Core.Api.Dto.Validators;

public class CredentialValidator : AbstractValidator<Credential>
{
  public CredentialValidator()
  {
    this.RuleFor(c => c.CredentialType).NotNull().DependentRules(() => {
      this.RuleFor(c => c.CredentialType!.Id).NotEmpty();
    });

    this.RuleFor(c => c.Identifier).NotEmpty().MaximumLength(64);
    this.RuleFor(c => c.Secret).MaximumLength(1024);
    this.RuleFor(c => c.Extra).MaximumLength(1024);

    this.RuleSet(RuleSetName.Create, () => {
      this.RuleFor(c => c.User).NotNull().DependentRules(() => {
        this.RuleFor(c => c.User!.Id).GreaterThan(0);
      });
    });

    this.RuleSet(RuleSetName.Edit, () => {
      this.RuleFor(c => c.Id).NotEmpty();
    });
  }
}