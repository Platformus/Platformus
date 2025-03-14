// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;

namespace Platformus.Core.Api.Dto.Validators;

public class UserRoleValidator : AbstractValidator<UserRole>
{
  public UserRoleValidator()
  {
    this.RuleFor(ur => ur.User).NotNull().DependentRules(() => {
      this.RuleFor(ur => ur.User!.Id).GreaterThan(0);
    });

    this.RuleFor(ur => ur.Role).NotNull().DependentRules(() => {
      this.RuleFor(ur => ur.Role!.Id).GreaterThan(0);
    });
  }
}