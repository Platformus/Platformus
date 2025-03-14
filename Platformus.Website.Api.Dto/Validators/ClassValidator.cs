// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using FluentValidation;
using Magicalizer.Validators.Abstractions;

namespace Platformus.Website.Api.Dto.Validators;

public class ClassValidator : AbstractValidator<Class>
{
  public ClassValidator()
  {
    this.RuleFor(c => c.SingularName).NotEmpty().MaximumLength(64);
    this.RuleFor(c => c.PluralName).NotEmpty().MaximumLength(64);
    this.RuleFor(c => c.UrlSegment).NotEmpty().MaximumLength(64);
    this.RuleFor(c => c.CSharpName).MaximumLength(32);
    this.When(c => c.Members != null, () => {
      this.RuleForEach(c => c.Members!).SetValidator(new MemberValidator(validateClass: false));
    });

    this.RuleSet(RuleSetName.Edit, () => {
      this.RuleFor(c => c.Id).NotEmpty();
    });
  }
}