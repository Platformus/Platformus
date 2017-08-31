// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.AspNetCore.Builder;

namespace Platformus.Security.Actions
{
  public class UseAuthenticationAction : IConfigureAction
  {
    public int Priority => 3000;

    public void Execute(IApplicationBuilder applicationBuilder, IServiceProvider serviceProvider)
    {
      applicationBuilder.UseAuthentication();
    }
  }
}