// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;

namespace Platformus.Domain.Backend.Controllers
{
  [Area("Backend")]
  public class ImageUploaderController : Platformus.Globalization.Backend.Controllers.ControllerBase
  {
    public IHostingEnvironment HostingEnvironment { get; private set; }

    public ImageUploaderController(IStorage storage, IHostingEnvironment hostingEnvironment)
      : base(storage)
    {
      this.HostingEnvironment = hostingEnvironment;
    }

    [HttpGet]
    public ActionResult Index()
    {
      return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(IList<IFormFile> files)
    {
      string filename = string.Empty;

      foreach (IFormFile source in files)
      {
        filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.Trim('"');
        filename = this.EnsureCorrectFilename(filename);

        using (FileStream output = System.IO.File.Create(this.GetPathAndFilename(filename)))
          await source.CopyToAsync(output);
      }

      return this.Content("filename=" + filename);
    }

    [HttpGet]
    public ActionResult GetCroppedImageUrl(string imageUrl, int x, int y, int width, int height, int sourceWidth, int sourceHeight)
    {
      // TODO: implement real cropping here
      return this.Content(imageUrl);
    }

    private string EnsureCorrectFilename(string filename)
    {
      if (filename.Contains("\\"))
        filename = filename.Substring(filename.LastIndexOf("\\") + 1);

      return filename;
    }

    private string GetPathAndFilename(string filename)
    {
      return this.HostingEnvironment.WebRootPath + "\\images\\temp\\" + filename;
    }
  }
}