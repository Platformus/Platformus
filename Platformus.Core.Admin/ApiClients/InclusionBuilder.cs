// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer;
using Magicalizer.Api.Dto.Abstractions;

namespace Platformus.Core.Admin.ApiServices;

public class InclusionBuilder<TDto> : PropertyPathBuilder<TDto, Inclusion<TDto>> where TDto : class, IDto
{
  public InclusionBuilder() : base(propertyPath => new Inclusion<TDto>(propertyPath))
  {
  }
}