// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;
using Magicalizer.Validators.Abstractions;

namespace Platformus.Core.Api.Dto.Validators;

public class RoleValidator : AbstractValidator<Role>
{
  public RoleValidator()
  {
    this.RuleFor(r => r.Code).NotEmpty().MaximumLength(32);
    this.RuleFor(r => r.Name).NotEmpty().MaximumLength(64);

    this.RuleSet(RuleSetName.Edit, () => {
      this.RuleFor(r => r.Id).NotEmpty();
    });
  }
}