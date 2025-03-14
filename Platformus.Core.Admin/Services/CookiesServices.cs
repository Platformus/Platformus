// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.JSInterop;
using Platformus.Core.Admin.Services.Abstractions;

namespace Platformus.Core.Admin.Services;

public class CookiesService : ICookiesService
{
  private readonly IJSRuntime jsRuntime;

  public CookiesService(IJSRuntime jsRuntime)
  {
    this.jsRuntime = jsRuntime;
  }

  public async Task<string?> GetCookieAsync(string name)
  {
    return await jsRuntime.InvokeAsync<string>("getCookie", name);
  }

  public async Task SetCookieAsync(string name, string value, int days)
  {
    await jsRuntime.InvokeVoidAsync("setCookie", name, value, days);
  }

  public async Task DeleteCookieAsync(string name)
  {
    await this.SetCookieAsync(name, string.Empty, -1);
  }
}
