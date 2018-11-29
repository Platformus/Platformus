// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using ExtCore.Data.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Platformus.Images.Controllers
{
  public class ImagesController : Platformus.Barebone.Controllers.ControllerBase
  {
    public class LoadImageFromUrlResult : IDisposable
    {
      public Image<Rgba32> Image { get; set; }
      public IImageFormat ImageFormat { get; set; }

      public LoadImageFromUrlResult(Image<Rgba32> image, IImageFormat imageFormat)
      {
        this.Image = image;
        this.ImageFormat = imageFormat;
      }

      public void Dispose()
      {
        this.Image.Dispose();
      }
    }

    private IHostingEnvironment hostingEnvironment;

    public ImagesController(IStorage storage, IHostingEnvironment hostingEnvironment)
      : base(storage)
    {
      this.hostingEnvironment = hostingEnvironment;
    }

    [HttpGet]
    public async Task<ActionResult> Index(string url, int w, int h, int q = 90)
    {
      string filename = url.Substring(url.LastIndexOf("/") + 1);

      using (LoadImageFromUrlResult result = await this.LoadImageFromUrlAsync(url))
      {
        if (result == null || result.Image == null || result.ImageFormat == null)
          return this.NotFound();

        Stream output = new MemoryStream();

        if (result.Image.Width != w || result.Image.Height != h)
          result.Image.Mutate(i => i.Resize(w, h));

        if (string.Equals(result.ImageFormat.Name, "Jpeg", StringComparison.OrdinalIgnoreCase))
          result.Image.Save(output, new JpegEncoder() { Quality = q });

        else result.Image.Save(output, new PngEncoder());

        output.Seek(0, SeekOrigin.Begin);
        return this.File(output, result.ImageFormat.DefaultMimeType);
      }
    }

    private async Task<LoadImageFromUrlResult> LoadImageFromUrlAsync(string url)
    {
      if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
        url = $"{this.Request.Scheme}://{this.Request.Host}{url}";

      try
      {
        using (HttpClient httpClient = new HttpClient())
        using (HttpResponseMessage response = await httpClient.GetAsync(url))
        using (Stream inputStream = await response.Content.ReadAsStreamAsync())
          return new LoadImageFromUrlResult(Image.Load(inputStream, out IImageFormat imageFormat), imageFormat);
      }

      catch (Exception e)
      {
        return null;
      }
    }
  }
}