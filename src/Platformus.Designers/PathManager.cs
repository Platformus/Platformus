// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Platformus.Barebone;

namespace Platformus.Designers
{
  public static class PathManager
  {
    private static string contentRootPath;
    private static string viewsPath;
    private static string stylesPath;
    private static string scriptsPath;
    private static string bundlesPath;

    public static string GetContentRootPath(IRequestHandler requestHandler)
    {
      if (string.IsNullOrEmpty(PathManager.contentRootPath))
      {
        IHostingEnvironment hostingEnvironment = requestHandler.GetService<IHostingEnvironment>();

        PathManager.contentRootPath = hostingEnvironment.ContentRootPath;
      }

      return PathManager.contentRootPath;
    }

    public static string GetViewsPath(IRequestHandler requestHandler, string subdirectory)
    {
      if (string.IsNullOrEmpty(PathManager.viewsPath))
        PathManager.viewsPath = PathManager.Combine(PathManager.GetContentRootPath(requestHandler), "Views");

      if (string.IsNullOrEmpty(subdirectory))
      {
        PathManager.EnsurePathExists(PathManager.viewsPath);
        return PathManager.viewsPath;
      }

      string viewsPath = PathManager.Combine(PathManager.viewsPath, subdirectory);

      PathManager.EnsurePathExists(viewsPath);
      return viewsPath;
    }

    public static string GetViewPath(IRequestHandler requestHandler, string subdirectory, string filename)
    {
      return PathManager.Combine(PathManager.GetViewsPath(requestHandler, subdirectory), filename);
    }

    public static string GetStylesPath(IRequestHandler requestHandler)
    {
      if (string.IsNullOrEmpty(PathManager.stylesPath))
        PathManager.stylesPath = PathManager.Combine(PathManager.GetContentRootPath(requestHandler), "Styles");

      PathManager.EnsurePathExists(PathManager.stylesPath);
      return PathManager.stylesPath;
    }

    public static string GetStylePath(IRequestHandler requestHandler, string filename)
    {
      return PathManager.Combine(PathManager.GetStylesPath(requestHandler), filename);
    }

    public static string GetScriptsPath(IRequestHandler requestHandler)
    {
      if (string.IsNullOrEmpty(PathManager.scriptsPath))
        PathManager.scriptsPath = PathManager.Combine(PathManager.GetContentRootPath(requestHandler), "Scripts");

      PathManager.EnsurePathExists(PathManager.scriptsPath);
      return PathManager.scriptsPath;
    }

    public static string GetScriptPath(IRequestHandler requestHandler, string filename)
    {
      return PathManager.Combine(PathManager.GetScriptsPath(requestHandler), filename);
    }

    public static string GetBundlesPath(IRequestHandler requestHandler)
    {
      if (string.IsNullOrEmpty(PathManager.bundlesPath))
        PathManager.bundlesPath = PathManager.Combine(PathManager.GetContentRootPath(requestHandler), "Bundles");

      PathManager.EnsurePathExists(PathManager.bundlesPath);
      return PathManager.bundlesPath;
    }

    public static string GetBundlePath(IRequestHandler requestHandler, string filename)
    {
      return PathManager.Combine(PathManager.GetBundlesPath(requestHandler), filename);
    }

    public static void EnsurePathExists(string path)
    {
      if (!Directory.Exists(path))
        Directory.CreateDirectory(path);
    }

    public static void EnsureFilepathExists(string filepath)
    {
      PathManager.EnsurePathExists(filepath.Remove(filepath.LastIndexOf(Path.DirectorySeparatorChar)));
    }

    public static string Combine(params string[] segments)
    {
      return string.Join(
        Path.DirectorySeparatorChar.ToString(),
        segments.Where(s => !string.IsNullOrEmpty(s)).Select(
          s => string.Join(Path.DirectorySeparatorChar.ToString(), s.Split(new char[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries))
        ).Where(s => !string.IsNullOrEmpty(s))
      );
    }
  }
}