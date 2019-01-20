// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Domain.Services.Abstractions;
using Platformus.Domain.Services.Defaults;

namespace Platformus.Domain.Actions
{
  public class AddDomainManagerAction : IConfigureServicesAction
  {
    public int Priority => 6000;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
      services.AddScoped<IDomainManager, DefaultDomainManager>();
    }
  }
}