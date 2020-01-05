// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;
using Microsoft.AspNetCore.Hosting;
using Platformus.Barebone;

namespace Platformus.ExtensionManager.Backend
{
  public static class PathManager
  {
    private static string contentRootPath;

    public static string GetContentRootPath(IRequestHandler requestHandler)
    {
      if (string.IsNullOrEmpty(PathManager.contentRootPath))
      {
        IWebHostEnvironment webHostEnvironment = requestHandler.GetService<IWebHostEnvironment>();

        PathManager.contentRootPath = webHostEnvironment.ContentRootPath;
      }

      return PathManager.contentRootPath;
    }

    public static string GetExtensionsPath(IRequestHandler requestHandler)
    {
      return new ExtensionManager(requestHandler).ExtensionsPath;
    }

    public static string GetExtensionPath(IRequestHandler requestHandler, string filename)
    {
      return PathManager.GetExtensionsPath(requestHandler) + Path.DirectorySeparatorChar + filename;
    }
  }
}