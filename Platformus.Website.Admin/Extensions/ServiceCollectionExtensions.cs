// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.Extensions.DependencyInjection;
using Platformus.Website.Admin.Services;

namespace Platformus.Website.Admin.Extensions;

public static class ServiceCollectionExtensions
{
  public static void AddPlatformusWebsiteAdmin(this IServiceCollection services)
  {
    services.AddScoped<ClassCache>();
    services.AddScoped<Core.Admin.IMetadata, Metadata>();
  }
}