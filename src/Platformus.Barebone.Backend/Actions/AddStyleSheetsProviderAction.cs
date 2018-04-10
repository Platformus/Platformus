// Copyright © 2018 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Barebone.Backend.Metadata.Providers;

namespace Platformus.Barebone.Backend.Actions
{
  public class AddStyleSheetsProviderAction : IConfigureServicesAction
  {
    public int Priority => 1000;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
      services.AddSingleton(typeof(IStyleSheetsProvider), typeof(DefaultStyleSheetsProvider));
    }
  }
}