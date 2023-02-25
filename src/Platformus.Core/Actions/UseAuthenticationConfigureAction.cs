// Copyright © 2020 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ExtCore.Infrastructure.Actions;
using Microsoft.AspNetCore.Builder;

namespace Platformus.Core.Actions;

public class UseAuthenticationConfigureAction : IConfigureAction
{
  public int Priority => 10010;

  public void Execute(IApplicationBuilder applicationBuilder, IServiceProvider serviceProvider)
  {
    applicationBuilder.UseAuthentication();
  }
}