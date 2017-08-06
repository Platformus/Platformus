// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.Extensions.DependencyInjection;

namespace Platformus.Routing.Actions
{
  public class AddMicrocontrollerResolverAction : IConfigureServicesAction
  {
    public int Priority => 4000;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
      services.AddScoped(typeof(IMicrocontrollerResolver), typeof(DefaultMicrocontrollerResolver));
    }
  }
}