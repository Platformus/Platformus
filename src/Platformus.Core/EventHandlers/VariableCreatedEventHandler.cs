﻿// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Platformus.Core.Data.Entities;
using Platformus.Core.Events;
using Platformus.Core.Services.Abstractions;

namespace Platformus.Core.EventHandlers
{
  public class VariableCreatedEventHandler : IVariableCreatedEventHandler
  {
    public int Priority => 1000;

    public void HandleEvent(HttpContext httpContext, Variable variable)
    {
      httpContext.RequestServices.GetService<IConfigurationManager>().InvalidateCache();
    }
  }
}