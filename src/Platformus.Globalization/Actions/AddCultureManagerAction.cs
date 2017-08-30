// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.Extensions.DependencyInjection;

namespace Platformus.Globalization.Actions
{
  public class AddCultureManagerAction : IConfigureServicesAction
  {
    public int Priority => 3500;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
      services.AddScoped<ICultureManager, DefaultCultureManager>();
    }
  }
}