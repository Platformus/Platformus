// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Website.Frontend.Services.Abstractions;
using Platformus.Website.Frontend.Services.Defaults;

namespace Platformus.Website.Frontend.Actions
{
  public class AddEndpointResolverAction : IConfigureServicesAction
  {
    public int Priority => 1000;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
      services.AddScoped(typeof(IEndpointResolver), typeof(DefaultEndpointResolver));
    }
  }
}