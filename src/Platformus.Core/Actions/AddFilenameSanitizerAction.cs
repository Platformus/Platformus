// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Core.Services.Abstractions;
using Platformus.Core.Services.Defaults;

namespace Platformus.Core.Actions
{
  public class AddFilenameSanitizerAction : IConfigureServicesAction
  {
    public int Priority => 1000;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
      services.AddSingleton(typeof(IFilenameSanitizer), typeof(DefaultFilenameSanitizer));
    }
  }
}