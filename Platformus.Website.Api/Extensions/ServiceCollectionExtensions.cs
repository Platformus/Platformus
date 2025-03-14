// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Reflection;
using System.Runtime.Loader;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Website.Domain.Services;

namespace Platformus.Website.Api.Extensions;

public static class ServiceCollectionExtensions
{
  public static void AddPlatformusWebsiteApi(this IServiceCollection services)
  {
    AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("Platformus.Website.Data.Entities"));
    AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("Platformus.Website.Domain.Models"));
    AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("Platformus.Website.Domain.Services"));
    AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName("Platformus.Website.Api.Dto"));
    services.AddScoped(typeof(StronglyTypedObjectService<>));
  }
}