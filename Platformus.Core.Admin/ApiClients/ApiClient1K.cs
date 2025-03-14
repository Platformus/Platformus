// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Net.Http.Json;
using System.Reflection;
using Magicalizer.Api.Dto.Abstractions;
using Magicalizer.Filters.Abstractions;
using Platformus.Core.Admin.Extensions;
using Platformus.Core.Admin.Services.Abstractions;

namespace Platformus.Core.Admin.ApiServices;

public class ApiClient1K<TKey, TDto, TFilter> : IApiClient1K<TKey, TDto, TFilter>
  where TDto : class, IDto
  where TFilter: class, IFilter
{
  private readonly HttpClient httpClient;
  private readonly string urlSegment;

  public ApiClient1K(HttpClient httpClient) : this(httpClient, typeof(TDto).GetCustomAttribute<MagicalizedAttribute>()?.Route ?? string.Empty) { }

  public ApiClient1K(HttpClient httpClient, string urlSegment)
  {
    this.httpClient = httpClient;
    this.urlSegment = urlSegment;
  }

  public virtual async Task<TDto?> GetByIdAsync(TKey id, params Inclusion<TDto>[] inclusions)
  {
    string uri = $"{urlSegment}/{id}?fields={string.Join(',', inclusions.Select(i => i.PropertyPath).ToList())}";

    return await httpClient.GetFromJsonAsync<TDto>(uri);
  }

  public virtual async Task<IPagedEnumerable<TDto>?> GetAllAsync(TFilter? filter = null, string? sorting = null, int? offset = null, int? limit = null, params Inclusion<TDto>[] inclusions)
  {
    string uri = $"{urlSegment}?{filter?.ToQueryString()}&sorting={sorting}&offset={offset ?? 0}&limit={limit ?? 10}&fields={string.Join(',', inclusions.Select(i => i.PropertyPath).ToList())}";
    HttpResponseMessage response = await httpClient.GetAsync(uri);

    if (!response.IsSuccessStatusCode)
      return null;

    IEnumerable<TDto>? items = await response.Content.ReadFromJsonAsync<IEnumerable<TDto>>();

    if (items == null) return null;

    int pagingTotalCount = 0, pagingOffset = 0, pagingLimit = 0;

    if (response.Headers.TryGetValues("Paging-Total-Count", out var pagingTotalCountValues))
      int.TryParse(pagingTotalCountValues.FirstOrDefault(), out pagingTotalCount);

    if (response.Headers.TryGetValues("Paging-Offset", out var pagingOffsetValues))
      int.TryParse(pagingOffsetValues.FirstOrDefault(), out pagingOffset);

    if (response.Headers.TryGetValues("Paging-Limit", out var pagingLimitValues))
      int.TryParse(pagingLimitValues.FirstOrDefault(), out pagingLimit);

    return new PagedList<TDto>(items, pagingTotalCount, pagingOffset, pagingLimit);
  }

  public virtual async Task<TDto?> PostAsync(TDto dto)
  {
    HttpResponseMessage response = await httpClient.PostAsJsonAsync(urlSegment, dto);

    if (response.IsSuccessStatusCode)
      return await response.Content.ReadFromJsonAsync<TDto>();

    return null;
  }

  public virtual async Task<bool> PutAsync(TDto dto)
  {
    HttpResponseMessage response = await httpClient.PutAsJsonAsync(urlSegment, dto);

    return response.IsSuccessStatusCode;
  }

  public virtual async Task<bool> DeleteAsync(TKey id)
  {
    HttpResponseMessage response = await httpClient.DeleteAsync($"{urlSegment}/{id}");

    return response.IsSuccessStatusCode;
  }
}