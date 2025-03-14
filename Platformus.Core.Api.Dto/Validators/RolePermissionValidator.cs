// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;

namespace Platformus.Core.Api.Dto.Validators;

public class RolePermissionValidator : AbstractValidator<RolePermission>
{
  public RolePermissionValidator()
  {
    this.RuleFor(rp => rp.Role).NotNull().DependentRules(() => {
      this.RuleFor(rp => rp.Role!.Id).GreaterThan(0);
    });

    this.RuleFor(rp => rp.Permission).NotNull().DependentRules(() => {
      this.RuleFor(rp => rp.Permission!.Id).GreaterThan(0);
    });
  }
}