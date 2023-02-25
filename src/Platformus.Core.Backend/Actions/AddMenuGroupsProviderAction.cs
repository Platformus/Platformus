// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Core.Backend.Metadata.Providers;

namespace Platformus.Core.Backend.Actions;

public class AddMenuGroupsProviderAction : IConfigureServicesAction
{
  public int Priority => 1000;

  public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
  {
    services.AddSingleton(typeof(IMenuGroupsProvider), typeof(DefaultMenuGroupsProvider));
  }
}