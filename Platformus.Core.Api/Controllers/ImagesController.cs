// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Api.Services.Abstractions;

namespace Platformus.Core.Api.Controllers;

[Route("api/v1/images")]
public class ImagesController : Controller
{
  private IFileManager fileManager;

  public ImagesController(IFileManager fileManager)
  {
    this.fileManager = fileManager;
  }

  [HttpPost]
  public async Task<ActionResult<Dto.Image>> PostAsync(IFormFile image)
  {
    if (image == null || image.Length == 0)
      return this.BadRequest();

    string filepath = Path.Combine("temp", image.FileName);

    await this.fileManager.WriteAsync(filepath, image);
    return new Dto.Image { Filename = image.FileName, Size = image.Length };
  }
}
