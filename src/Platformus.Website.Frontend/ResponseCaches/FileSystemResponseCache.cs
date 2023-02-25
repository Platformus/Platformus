// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Core.Parameters;
using Platformus.Website.ResponseCaches;

namespace Platformus.Website.Frontend;

public class FileSystemResponseCache : IResponseCache
{
  private const string cache = "Cache";
  private readonly ConcurrentDictionary<string, object> locks = new ConcurrentDictionary<string, object>();

  public string Description => "Caches responses in filesystem.";
  public IEnumerable<ParameterGroup> ParameterGroups => new ParameterGroup[] { };

  public async Task<byte[]> GetWithDefaultValueAsync(HttpContext httpContext, Func<Task<byte[]>> defaultValueFunc)
  {
    IWebHostEnvironment webHostEnvironment = httpContext.RequestServices.GetService<IWebHostEnvironment>();
    string path = Path.Combine(webHostEnvironment.ContentRootPath, cache, this.GenerateUniqueKeyForUrl(httpContext.Request.GetEncodedPathAndQuery()));
    byte[] responseBody = null;

    await Task.Factory.StartNew(() =>
    {
      lock (this.locks.GetOrAdd(path, () => new object()))
      {
        if (File.Exists(path))
          responseBody = File.ReadAllBytes(path);

        else
        {
          responseBody = defaultValueFunc().Result;

          if (responseBody != null)
            File.WriteAllBytes(path, responseBody);
        }
      }
    });

    return responseBody;
  }

  public async Task RemoveAllAsync(HttpContext httpContext)
  {
    IWebHostEnvironment webHostEnvironment = httpContext.RequestServices.GetService<IWebHostEnvironment>();
    string path = Path.Combine(webHostEnvironment.ContentRootPath, cache);

    await Task.Factory.StartNew(() =>
    {
      lock (this.locks.GetOrAdd(path, () => new object()))
      {
        if (Directory.Exists(path))
          foreach (string filepath in Directory.EnumerateFiles(path))
            File.Delete(filepath);
      }
    });
  }

  private string GenerateUniqueKeyForUrl(string url)
  {
    return url.Replace('/', '_').Replace('?', '_').Replace('&', '_').Replace('=', '_');
  }
}