// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq.Expressions;
using Magicalizer.Api.Dto.Abstractions;

namespace Platformus.Core.Admin.ApiServices;

public class Inclusion<TDto> where TDto : class, IDto
{
  public string PropertyPath { get; }

  public Inclusion(Expression<Func<TDto, object?>> property)
  {
    this.PropertyPath = property.GetPropertyPath();
  }

  public Inclusion(string propertyPath)
  {
    this.PropertyPath = propertyPath;
  }
}