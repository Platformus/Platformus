// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO.Compression;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Platformus.Barebone;

namespace Platformus.ExtensionManager
{
  public class ExtensionManager
  {
    private IRequestHandler requestHandler;

    private static string extensionsPath;

    public string ExtensionsPath
    {
      get
      {
        if (string.IsNullOrEmpty(ExtensionManager.extensionsPath))
        {
          IHostingEnvironment hostingEnvironment = this.requestHandler.GetService<IHostingEnvironment>();
          IConfigurationRoot configurationRoot = new ConfigurationBuilder()
            .SetBasePath(hostingEnvironment.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();

          string extensionsPath = configurationRoot?["Extensions:Path"];

          if (string.IsNullOrEmpty(extensionsPath))
            return null;

          ExtensionManager.extensionsPath = hostingEnvironment.ContentRootPath + extensionsPath;
        }

        return ExtensionManager.extensionsPath;
      }
    }

    public ExtensionManager(IRequestHandler requestHandler)
    {
      this.requestHandler = requestHandler;
    }

    public string[] UnzipExtensionArchive(string filepath)
    {
      try
      {
        ZipFile.ExtractToDirectory(filepath, this.ExtensionsPath);
        System.IO.File.Delete(filepath);
      }

      catch { }

      return null;
    }

    public Extension ReadExtension(string filepath)
    {
      return JsonConvert.DeserializeObject<Extension>(System.IO.File.ReadAllText(filepath));
    }
  }
}