// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace Platformus.Installer
{
  public class ResourceManager
  {
    public static Installation GetInstallation()
    {
      return JsonConvert.DeserializeObject<Platformus.Installer.Installation>(
        ResourceManager.GetResourceString("Platformus.Installer.Input.installation.json")
      );
    }

    public static string GetAppSettingsTemplate()
    {
      return ResourceManager.GetResourceString("Platformus.Installer.Input.appsettings.json.template");
    }

    public static string GetResourceString(string resourceName)
    {
      using (Stream resourceStream = typeof(ResourceManager).Assembly.GetManifestResourceStream(resourceName))
        using (StreamReader reader = new StreamReader(resourceStream, Encoding.UTF8))
          return reader.ReadToEnd();
    }

    public static void WriteResourceToFile(string resourceName, string destinationPath)
    {
      using (Stream resourceStream = typeof(ResourceManager).Assembly.GetManifestResourceStream(resourceName))
        using (FileStream fileStream = System.IO.File.Create(destinationPath))
          resourceStream.CopyTo(fileStream);
    }
  }
}