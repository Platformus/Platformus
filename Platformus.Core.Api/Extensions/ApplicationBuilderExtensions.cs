// Copyright © 2025 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Reflection;
using Magicalizer.Extensions;
using Microsoft.AspNetCore.Builder;

namespace Platformus.Core.Api.Extensions;
public static class WebApplicationExtensions
{
  public static void UsePlatformusCoreApi(this WebApplication app, params Assembly[] additionalAssemblies)
  {
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMagicalizer();
  }
}