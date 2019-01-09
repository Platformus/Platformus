// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Security.Services.Abstractions;
using Platformus.Security.Services.Default;

namespace Platformus.Security.Actions
{
  public class AddUserManagerAction : IConfigureServicesAction
  {
    public int Priority => 3020;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
      services.AddScoped(typeof(IUserManager), typeof(DefaultUserManager));
    }
  }
}