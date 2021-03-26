// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Website.ResponseCaches;

namespace Platformus.Website.Frontend
{
  public class FileSystemResponseCache : IResponseCache
  {
    private const string cache = "Cache";

    public async Task<byte[]> GetWithDefaultValueAsync(HttpContext httpContext, Func<Task<byte[]>> defaultValueFunc)
    {
      IWebHostEnvironment webHostEnvironment = httpContext.RequestServices.GetService<IWebHostEnvironment>();
      string path = Path.Combine(webHostEnvironment.ContentRootPath, cache, this.GenerateUniqueKeyForUrl(httpContext.Request.GetEncodedPathAndQuery()));

      if (File.Exists(path))
        return await File.ReadAllBytesAsync(path);

      byte[] responseBody = await defaultValueFunc();

      await File.WriteAllBytesAsync(path, responseBody);
      return responseBody;
    }

    public async Task RemoveAllAsync(HttpContext httpContext)
    {
      IWebHostEnvironment webHostEnvironment = httpContext.RequestServices.GetService<IWebHostEnvironment>();
      string path = Path.Combine(webHostEnvironment.ContentRootPath, cache);

      // TODO: handle parallel requests
      if (Directory.Exists(path))
        await Task.Factory.StartNew(() => {
          foreach (string filepath in Directory.EnumerateFiles(path))
            File.Delete(filepath);
        });
    }

    private string GenerateUniqueKeyForUrl(string url)
    {
      return url.Replace('/', '_').Replace('?', '_').Replace('&', '_').Replace('=', '_');
    }
  }
}