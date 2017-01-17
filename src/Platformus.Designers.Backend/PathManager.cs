// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Barebone;

namespace Platformus.Designers.Backend
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
        IHostingEnvironment hostingEnvironment = requestHandler.HttpContext.RequestServices.GetService<IHostingEnvironment>();

        PathManager.contentRootPath = hostingEnvironment.ContentRootPath;
      }

      return PathManager.contentRootPath;
    }

    public static string GetViewsPath(IRequestHandler requestHandler, string subdirectory)
    {
      if (string.IsNullOrEmpty(PathManager.viewsPath))
        PathManager.viewsPath = PathManager.GetContentRootPath(requestHandler) + "\\Views";

      if (string.IsNullOrEmpty(subdirectory))
        return PathManager.viewsPath;

      return PathManager.viewsPath + "\\" + subdirectory;
    }

    public static string GetViewPath(IRequestHandler requestHandler, string subdirectory, string filename)
    {
      return PathManager.GetViewsPath(requestHandler, subdirectory) + "\\" + filename;
    }

    public static string GetStylesPath(IRequestHandler requestHandler)
    {
      if (string.IsNullOrEmpty(PathManager.stylesPath))
        PathManager.stylesPath = PathManager.GetContentRootPath(requestHandler) + "\\Styles";

      return PathManager.stylesPath;
    }

    public static string GetStylePath(IRequestHandler requestHandler, string filename)
    {
      return PathManager.GetStylesPath(requestHandler) + "\\" + filename;
    }

    public static string GetScriptsPath(IRequestHandler requestHandler)
    {
      if (string.IsNullOrEmpty(PathManager.scriptsPath))
        PathManager.scriptsPath = PathManager.GetContentRootPath(requestHandler) + "\\Scripts";

      return PathManager.scriptsPath;
    }

    public static string GetScriptPath(IRequestHandler requestHandler, string filename)
    {
      return PathManager.GetScriptsPath(requestHandler) + "\\" + filename;
    }

    public static string GetBundlesPath(IRequestHandler requestHandler)
    {
      if (string.IsNullOrEmpty(PathManager.bundlesPath))
        PathManager.bundlesPath = PathManager.GetContentRootPath(requestHandler) + "\\Bundles";

      return PathManager.bundlesPath;
    }

    public static string GetBundlePath(IRequestHandler requestHandler, string filename)
    {
      return PathManager.GetBundlesPath(requestHandler) + "\\" + filename;
    }
  }
}