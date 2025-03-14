// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Net.Http.Json;

namespace Platformus.Core.Admin.ApiServices;

public class AccessTokenApiClient
{
  private readonly HttpClient httpClient;

  public AccessTokenApiClient(HttpClient httpClient)
  {
    this.httpClient = httpClient;
  }

  public async Task<Api.Dto.AccessToken.Get.AccessToken?> PostAsync(Api.Dto.AccessToken.Post.AccessToken accessToken)
  {
    HttpResponseMessage response = await httpClient.PostAsJsonAsync("/api/v1/access-tokens", accessToken);

    if (response.IsSuccessStatusCode)
      return await response.Content.ReadFromJsonAsync<Api.Dto.AccessToken.Get.AccessToken>();

    return null;
  }
}