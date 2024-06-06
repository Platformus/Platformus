// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;

namespace Platformus.Images.Controllers;

public class ImagesController : Controller
{
  private IWebHostEnvironment webHostEnvironment;

  public ImagesController(IWebHostEnvironment webHostEnvironment)
  {
    this.webHostEnvironment = webHostEnvironment;
  }

  [HttpGet]
  public async Task<IActionResult> IndexAsync(string url, [FromQuery] Rectangle source = null, [FromQuery] Size destination = null, string format = null, int quality = 90, string copyTo = null)
  {
    Image result = await this.LoadImageFromUrlAsync(url);

    this.ProcessImage(result, source, destination);

    IImageEncoder imageEncoder = this.GetImageEncoder(result.Metadata.DecodedImageFormat, format, quality);

    if (!string.IsNullOrEmpty(copyTo))
    {
      string filename = this.GetFilenameFromUrl(url);
      string destinationFilepath = this.GetDestinationFilepath(copyTo, filename);

      await result.SaveAsync(
        destinationFilepath,
        imageEncoder
      );

      this.Response.Headers.Add("DestinationUrl", this.GetDestinationUrl(copyTo, filename));
    }

    Stream output = new MemoryStream();

    await result.SaveAsync(output, imageEncoder);
    result.Dispose();
    output.Seek(0, SeekOrigin.Begin);
    return this.File(output, this.GetImageMimeType(result.Metadata.DecodedImageFormat, format));
  }

  private async Task<Image> LoadImageFromUrlAsync(string url)
  {
    if (!Uri.IsWellFormedUriString(url, UriKind.Absolute))
      url = $"{this.Request.Scheme}://{this.Request.Host}{url}";

    try
    {
      using (HttpClient httpClient = new HttpClient())
      using (HttpResponseMessage response = await httpClient.GetAsync(url))
      using (Stream inputStream = await response.Content.ReadAsStreamAsync())
        return await Image.LoadAsync(inputStream);
    }

    catch (Exception e)
    {
      return null;
    }
  }

  private void ProcessImage(Image image, Rectangle source, Size destination)
  {
    image.Mutate(i =>
    {
      i.AutoOrient();

      if (!source.IsEmpty())
        i.Crop(new SixLabors.ImageSharp.Rectangle(source.X, source.Y, source.Width, source.Height));

      if (!destination.IsEmpty())
        i.Resize(new SixLabors.ImageSharp.Size(destination.Width, destination.Height));
    });
  }

  private IImageEncoder GetImageEncoder(IImageFormat imageFormat, string destinationImageFormatName, int destinationImageQuality)
  {
    if (string.IsNullOrEmpty(destinationImageFormatName))
      destinationImageFormatName = imageFormat.Name;

    if (string.Equals(destinationImageFormatName, "Gif", StringComparison.OrdinalIgnoreCase))
      return new GifEncoder();

    else if (string.Equals(destinationImageFormatName, "Jpeg", StringComparison.OrdinalIgnoreCase))
      return new JpegEncoder() { Quality = destinationImageQuality };

    else return new PngEncoder();
  }

  private string GetImageMimeType(IImageFormat imageFormat, string destinationImageFormatName)
  {
    if (string.IsNullOrEmpty(destinationImageFormatName))
      destinationImageFormatName = imageFormat.Name;

    if (string.Equals(destinationImageFormatName, "Gif", StringComparison.OrdinalIgnoreCase))
      return "image/gif";

    else if (string.Equals(destinationImageFormatName, "Jpeg", StringComparison.OrdinalIgnoreCase))
      return "image/jpeg";

    else return "image/png";
  }

  private string GetFilenameFromUrl(string url)
  {
    return url.Split('/').Last();
  }

  private string GetDestinationFilepath(string destinationBaseUrl, string filename)
  {
    string destinationPath = destinationBaseUrl.Trim('/').Replace('/', Path.DirectorySeparatorChar);

    return Path.Combine(this.webHostEnvironment.WebRootPath, destinationPath, filename);
  }

  private string GetDestinationUrl(string destinationBaseUrl, string filename)
  {
    return '/' + destinationBaseUrl.Trim('/') + '/' + filename;
  }
}