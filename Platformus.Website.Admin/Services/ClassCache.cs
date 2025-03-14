// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Platformus.Core.Admin.Services.Abstractions;
using Platformus.Website.Api.Dto;
using Platformus.Website.Filters;

namespace Platformus.Website.Admin.Services;

public class ClassCache
{
  private static IEnumerable<Class>? classes;

  private readonly IApiClient1K<int, Class, ClassFilter> classApiClient;

  public ClassCache(IApiClient1K<int, Class, ClassFilter> classApiClient)
  {
    this.classApiClient = classApiClient;
  }

  public async Task<IEnumerable<Class>> GetClassesAsync()
  {
    if (classes is null)
      classes = await this.classApiClient.GetAllAsync();

    return classes!;
  }
}