﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace Platformus.Core.Actions;

public class AddStringLocalizerFactoryAction : IConfigureServicesAction
{
  public int Priority => 1000;

  public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
  {
    services.AddSingleton<IStringLocalizerFactory, StringLocalizerFactory>();
  }
}