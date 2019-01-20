// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Configurations.Services.Abstractions;
using Platformus.Configurations.Services.Defaults;

namespace Platformus.Configurations.Actions
{
  public class AddConfigurationManagerAction : IConfigureServicesAction
  {
    public int Priority => 2000;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
      services.AddScoped<IConfigurationManager, DefaultConfigurationManager>();
    }
  }
}