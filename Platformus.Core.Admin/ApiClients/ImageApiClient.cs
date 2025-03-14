// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;

namespace Platformus.Core.Admin.ApiServices;

public class ImageApiClient
{
  private readonly HttpClient httpClient;

  public ImageApiClient(HttpClient httpClient)
  {
    this.httpClient = httpClient;
  }

  public async Task<Api.Dto.Image?> PostAsync(IBrowserFile image)
  {
    using (MultipartFormDataContent content = new MultipartFormDataContent())
    {
      StreamContent file = new StreamContent(image.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024));

      file.Headers.ContentType = new MediaTypeHeaderValue(image.ContentType);
      content.Add(file, "image", image.Name);

      HttpResponseMessage response = await this.httpClient.PostAsync("api/v1/images", content);

      if (response.IsSuccessStatusCode)
        return await response.Content.ReadFromJsonAsync<Api.Dto.Image>();
    }

    return null;
  }
}