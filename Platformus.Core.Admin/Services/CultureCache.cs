// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Admin.Services.Abstractions;
using Platformus.Core.Api.Dto;
using Platformus.Core.Filters;

namespace Platformus.Core.Admin.Services;

public class CultureCache : ICultureCache
{
  private static IEnumerable<Culture>? cultures;

  private readonly IApiClient1K<string, Culture, CultureFilter> cultureApiClient;

  public CultureCache(IApiClient1K<string, Culture, CultureFilter> cultureApiClient)
  {
    this.cultureApiClient = cultureApiClient;
  }

  public async Task<IEnumerable<Culture>> GetCulturesAsync()
  {
    if (cultures is null)
      cultures = await this.cultureApiClient.GetAllAsync();

    return cultures!;
  }
}