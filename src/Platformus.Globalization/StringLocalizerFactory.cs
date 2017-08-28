// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.IO;
using System.Reflection;
using System.Resources;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Platformus.Globalization
{
  public class StringLocalizerFactory : IStringLocalizerFactory
  {
    private readonly IResourceNamesCache resourceNamesCache = new ResourceNamesCache();
    private readonly string resourcesRelativePath;
    private readonly ILoggerFactory loggerFactory;

    public StringLocalizerFactory(IOptions<LocalizationOptions> localizationOptions, ILoggerFactory loggerFactory)
    {
      resourcesRelativePath = localizationOptions.Value.ResourcesPath ?? string.Empty;

      if (!string.IsNullOrEmpty(resourcesRelativePath))
      {
        resourcesRelativePath = resourcesRelativePath
          .Replace(Path.AltDirectorySeparatorChar, '.')
          .Replace(Path.DirectorySeparatorChar, '.') + ".";
      }

      this.loggerFactory = loggerFactory;
    }

    public IStringLocalizer Create(Type resourceSource)
    {
      if (resourceSource == null)
        throw new ArgumentNullException(nameof(resourceSource));

      Assembly assembly = Assembly.GetEntryAssembly();
      string baseName = assembly.GetName().Name + "." + this.resourcesRelativePath + resourceSource.FullName;

      // TODO: add caching
      return this.CreateResourceManagerStringLocalizer(assembly, baseName);
    }

    public IStringLocalizer Create(string baseName, string location)
    {
      if (baseName == null)
        throw new ArgumentNullException(nameof(baseName));

      if (location == null)
        throw new ArgumentNullException(nameof(location));

      Assembly assembly = Assembly.GetEntryAssembly();

      baseName = baseName.Replace(assembly.GetName().Name + ".", assembly.GetName().Name + "." + this.resourcesRelativePath);

      // TODO: add caching
      return this.CreateResourceManagerStringLocalizer(assembly, baseName);
    }

    protected virtual ResourceManagerStringLocalizer CreateResourceManagerStringLocalizer(Assembly assembly, string baseName)
    {
      return new ResourceManagerStringLocalizer(
        new ResourceManager(baseName, assembly),
        assembly,
        baseName,
        this.resourceNamesCache,
        this.loggerFactory.CreateLogger<ResourceManagerStringLocalizer>()
      );
    }
  }
}