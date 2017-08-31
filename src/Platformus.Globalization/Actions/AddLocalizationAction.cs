// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.Extensions.DependencyInjection;

namespace Platformus.Globalization.Actions
{
  public class AddLocalizationAction : IConfigureServicesAction
  {
    public int Priority => 4000;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
      services.AddLocalization(
        localizationOptions =>
        {
          localizationOptions.ResourcesPath = "Resources";
        }
      );
    }
  }
}