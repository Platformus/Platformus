// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Magicalizer.Api.Dto.Abstractions;
using Magicalizer.Filters.Abstractions;
using Platformus.Core.Admin.ApiServices;

namespace Platformus.Core.Admin.Services.Abstractions;

public interface IApiClient2K<TKey1, TKey2, TDto, TFilter>
  where TDto : class, IDto
  where TFilter : class, IFilter
{
  Task<TDto?> GetByIdAsync(TKey1 id1, TKey2 id2, params Inclusion<TDto>[] inclusions);
  Task<IPagedEnumerable<TDto>?> GetAllAsync(TFilter? filter = null, string? sorting = null, int? offset = null, int? limit = null, params Inclusion<TDto>[] inclusions);
  Task<TDto?> PostAsync(TDto dto);
  Task<bool> PutAsync(TDto dto);
  Task<bool> DeleteAsync(TKey1 id1, TKey2 id2);
}