// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace Platformus.Barebone.Backend.Controllers
{
  [Area("Backend")]
  public class ImageUploaderController : ControllerBase
  {
    private IWebHostEnvironment webHostEnvironment;

    public ImageUploaderController(IStorage storage, IWebHostEnvironment webHostEnvironment)
      : base(storage)
    {
      this.webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public ActionResult Index()
    {
      return this.View();
    }

    [HttpPost]
    public async Task<IActionResult> Index(IList<IFormFile> files)
    {
      foreach (IFormFile source in files)
        using (FileStream output = System.IO.File.Create(this.GetTempFilepath(source.FileName)))
          await source.CopyToAsync(output);

      return this.Content("filename=" + string.Join(",", files.Select(f => f.FileName)));
    }

    [HttpGet]
    public ActionResult GetCroppedImageUrl(string sourceImageUrl, int sourceX, int sourceY, int sourceWidth, int sourceHeight, string destinationBaseUrl, int destinationWidth, int destinationHeight)
    {
      string filename = sourceImageUrl.Substring(sourceImageUrl.LastIndexOf("/") + 1);
      string tempFilepath = this.GetTempFilepath(filename);
      string destinationFilepath = this.GetFilepath(destinationBaseUrl, filename);

      using (Image image = this.LoadImageFromFile(tempFilepath, out IImageFormat imageFormat))
      {
        if (image.Width == destinationWidth && image.Height == destinationHeight)
        {
          if (System.IO.File.Exists(destinationFilepath))
            System.IO.File.Delete(destinationFilepath);

          System.IO.File.Move(tempFilepath, destinationFilepath);
        }

        else
        {
          image.Mutate(
            i => i.Crop(new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight)).Resize(destinationWidth, destinationHeight)
          );

          if (string.Equals(imageFormat.Name, "Jpeg", StringComparison.OrdinalIgnoreCase))
            image.Save(destinationFilepath, new JpegEncoder() { Quality = 80 });

          else image.Save(destinationFilepath);
        }

        return this.Content(destinationBaseUrl + filename);
      }
    }

    private Image LoadImageFromFile(string filepath, out IImageFormat imageFormat)
    {
      return Image.Load(filepath, out imageFormat);
    }

    private string GetTempFilepath(string filename)
    {
      return this.GetFilepath(this.GetTempBasePath(), filename);
    }

    private string GetFilepath(string basePath, string filename)
    {
      basePath = basePath.Replace('/', '\\');

      return this.webHostEnvironment.WebRootPath + basePath.Replace('\\', Path.DirectorySeparatorChar) + filename;
    }

    private string GetTempBasePath()
    {
      char directorySeparatorChar = Path.DirectorySeparatorChar;

      return $"{directorySeparatorChar}images{directorySeparatorChar}temp{directorySeparatorChar}";
    }
  }
}