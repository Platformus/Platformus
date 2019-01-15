// Copyright © 2019 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Messaging.Services.Abstractions;
using Platformus.Messaging.Services.Defaults;

namespace Platformus.Messaging.Actions
{
  public class AddEmailSenderAction : IConfigureServicesAction
  {
    public int Priority => 7000;

    public void Execute(IServiceCollection services, IServiceProvider serviceProvider)
    {
      services.AddScoped(typeof(IEmailSender), typeof(DefaultEmailSender));
    }
  }
}