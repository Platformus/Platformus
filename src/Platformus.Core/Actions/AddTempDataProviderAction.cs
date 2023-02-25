// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;

namespace Platformus.Core.Actions;

public class AddTempDataProviderAction : IConfigureServicesAction
{
  public int Priority => 1000;

  public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
  {
    services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
  }
}