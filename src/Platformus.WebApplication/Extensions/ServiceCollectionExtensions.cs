// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using ExtCore.WebApplication;
using ExtCore.WebApplication.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Platformus.WebApplication.Extensions
{
  public static class ServiceCollectionExtensions
  {
    public static void AddPlatformus(this IServiceCollection services)
    {
      services.AddExtCore(null);
    }

    public static void AddPlatformus(this IServiceCollection services, string extensionsPath)
    {
      services.AddExtCore(extensionsPath, true);
    }

    public static void AddPlatformus(this IServiceCollection services, string extensionsPath, IAssemblyProvider assemblyProvider)
    {
      services.AddExtCore(extensionsPath, true, assemblyProvider);
    }
  }
}