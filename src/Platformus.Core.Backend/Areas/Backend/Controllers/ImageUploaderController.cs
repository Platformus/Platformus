// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Magicalizer.Data.Repositories.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Platformus.Core.Services.Abstractions;

namespace Platformus.Core.Backend.Controllers;

public class ImageUploaderController : ControllerBase
{
  private IWebHostEnvironment webHostEnvironment;
  private IFilenameSanitizer filenameSanitizer;

  public ImageUploaderController(IStorage storage, IWebHostEnvironment webHostEnvironment, IFilenameSanitizer filenameSanitizer)
    : base(storage)
  {
    this.webHostEnvironment = webHostEnvironment;
    this.filenameSanitizer = filenameSanitizer;
  }

  [HttpGet]
  public IActionResult IndexAsync()
  {
    return this.View();
  }

  [HttpPost]
  public async Task<IActionResult> IndexAsync(IList<IFormFile> files)
  {
    if (files.Count() == 0)
      return this.BadRequest();

    IFormFile file = files.First();
    string filename = this.filenameSanitizer.SanitizeFilename(file.FileName);

    using (FileStream output = System.IO.File.Create(this.GetTempFilepath(filename)))
      await file.CopyToAsync(output);

    return this.Content("filename=" + filename);
  }

  private string GetTempFilepath(string filename)
  {
    return Path.Combine(this.webHostEnvironment.WebRootPath, "images", "temp", filename);
  }
}