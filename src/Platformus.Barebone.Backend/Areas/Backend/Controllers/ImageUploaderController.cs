// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.Primitives;

namespace Platformus.Barebone.Backend.Controllers
{
  [Area("Backend")]
  public class ImageUploaderController : ControllerBase
  {
    private IHostingEnvironment hostingEnvironment;

    public ImageUploaderController(IStorage storage, IHostingEnvironment hostingEnvironment)
      : base(storage)
    {
      this.hostingEnvironment = hostingEnvironment;
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
        filename = ContentDispositionHeaderValue.Parse(source.ContentDisposition).FileName.ToString().Trim('"');
        filename = this.EnsureCorrectFilename(filename);

        using (FileStream output = System.IO.File.Create(this.GetTempFilepath(filename)))
          await source.CopyToAsync(output);
      }

      return this.Content("filename=" + filename);
    }

    [HttpGet]
    public ActionResult GetCroppedImageUrl(string sourceImageUrl, int sourceX, int sourceY, int sourceWidth, int sourceHeight, string destinationImageBaseUrl, int destinationWidth, int destinationHeight)
    {
      string filename = sourceImageUrl.Substring(sourceImageUrl.LastIndexOf("/") + 1);

      using (Image<Rgba32> image = this.LoadImageFromFile(this.GetTempFilepath(filename), out IImageFormat imageFormat))
      {
        if (image.Width == destinationWidth && image.Height == destinationHeight)
          return this.Content(sourceImageUrl);

        image.Mutate(
          i => i.Crop(new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight)).Resize(destinationWidth, destinationHeight)
        );
        
        if (string.Equals(imageFormat.Name, "Jpeg", StringComparison.OrdinalIgnoreCase))
          image.Save(this.GetFilepath(destinationImageBaseUrl, filename), new JpegEncoder() { Quality = 80 });

        else image.Save(this.GetFilepath(destinationImageBaseUrl, filename));

        return this.Content(destinationImageBaseUrl + filename);
      }
    }

    private Image<Rgba32> LoadImageFromFile(string filepath, out IImageFormat imageFormat)
    {
      return Image.Load(filepath, out imageFormat);
    }

    private string EnsureCorrectFilename(string filename)
    {
      if (filename.Contains(Path.DirectorySeparatorChar.ToString()))
        filename = filename.Substring(filename.LastIndexOf(Path.DirectorySeparatorChar) + 1);

      return filename;
    }

    private string GetTempFilepath(string filename)
    {
      return this.GetFilepath(this.GetTempBasePath(), filename);
    }

    private string GetFilepath(string basePath, string filename)
    {
      basePath = basePath.Replace('/', '\\');

      return this.hostingEnvironment.WebRootPath + basePath.Replace('\\', Path.DirectorySeparatorChar) + filename;
    }

    private string GetTempBasePath()
    {
      char directorySeparatorChar = Path.DirectorySeparatorChar;

      return $"{directorySeparatorChar}images{directorySeparatorChar}temp{directorySeparatorChar}";
    }
  }
}