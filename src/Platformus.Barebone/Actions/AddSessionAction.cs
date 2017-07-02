// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.Extensions.DependencyInjection;

namespace Platformus.Barebone.Actions
{
  public class AddSessionAction : IConfigureServicesAction
  {
    public int Priority => 1010;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
      services.AddSession();
    }
  }
}