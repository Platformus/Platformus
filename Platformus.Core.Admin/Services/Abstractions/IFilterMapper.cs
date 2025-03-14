// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Filters.Abstractions;

namespace Platformus.Core.Admin.Services.Abstractions;

public interface IFilterMapper
{
  T Map<T>() where T : class, IFilter, new();
}