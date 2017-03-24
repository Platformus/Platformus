// Copyright © 2015 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Drawing;
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

        using (FileStream output = System.IO.File.Create(this.GetPathAndFilename("\\images\\temp\\", filename)))
          await source.CopyToAsync(output);
      }

      return this.Content("filename=" + filename);
    }

    [HttpGet]
    public ActionResult GetCroppedImageUrl(string sourceImageUrl, int sourceX, int sourceY, int sourceWidth, int sourceHeight, string destinationImageBaseUrl, int destinationWidth, int destinationHeight)
    {
      string filename = sourceImageUrl.Substring(sourceImageUrl.LastIndexOf("/") + 1);
      Image sourceImage = this.LoadImageFromFile(this.GetPathAndFilename("\\images\\temp\\", filename));

      if (sourceImage.Width == destinationWidth && sourceImage.Height == destinationHeight)
        return this.Content(sourceImageUrl);

      using (Image destinationImage = new Bitmap(destinationWidth, destinationHeight))
      {
        Graphics g = Graphics.FromImage(destinationImage);

        g.DrawImage(
          sourceImage,
          new Rectangle(0, 0, destinationWidth, destinationHeight),
          new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
          GraphicsUnit.Pixel
        );

        destinationImage.Save(this.GetPathAndFilename(destinationImageBaseUrl, filename));
        return this.Content(destinationImageBaseUrl + filename);
      }
    }

    private Image LoadImageFromFile(string pathAndFilename)
    {
      Image image = null;

      using (Bitmap temp = new Bitmap(pathAndFilename))
        image = new Bitmap(temp);

      return image;
    }

    private string EnsureCorrectFilename(string filename)
    {
      if (filename.Contains("\\"))
        filename = filename.Substring(filename.LastIndexOf("\\") + 1);

      return filename;
    }

    private string GetPathAndFilename(string basePath, string filename)
    {
      return this.HostingEnvironment.WebRootPath + basePath.Replace('/', '\\') + filename;
    }
  }
}